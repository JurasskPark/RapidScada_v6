// Contains factories and scripts for calendar components.

rs.mimic.CalendarSingleInputScript = class extends rs.mimic.ComponentScript {
    _oaToDate(oaVal) {
        // Convert OLE Automation date stored by Rapid SCADA to JavaScript Date.
        const ms = (oaVal - 25569) * 86400000;
        let dt = new Date(ms);
        return Number.isNaN(dt.valueOf()) ? null : dt;
    }

    _dateToLocalInput(date) {
        const pad = n => String(n).padStart(2, "0");
        return date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate()) + "T" +
            pad(date.getHours()) + ":" + pad(date.getMinutes());
    }

    _hexToBytes(hex) {
        // Convert hexadecimal command data to bytes for parsing double values.
        hex = String(hex || "").replace(/[^0-9a-f]/gi, "");
        if (hex.length < 16) {
            return null;
        }

        let bytes = new Uint8Array(Math.floor(hex.length / 2));
        for (let i = 0; i < bytes.length; i++) {
            bytes[i] = parseInt(hex.substr(i * 2, 2), 16);
        }
        return bytes;
    }

    _doubleFromHex(hex, offset) {
        // Read a little-endian double from hexadecimal command data.
        let bytes = this._hexToBytes(hex);
        if (!bytes || bytes.length < offset + 8) {
            return NaN;
        }

        return new DataView(bytes.buffer).getFloat64(offset, true);
    }

    _getDisplayValue(curData) {
        return curData?.df?.dispVal ?? curData?.d?.text ?? curData?.d?.valText ?? "";
    }

    _getDebugData(curData) {
        return {
            data: curData?.d ?? null,
            formatted: curData?.df ?? null,
            rawValue: curData?.d?.val,
            status: curData?.d?.stat,
            displayValue: this._getDisplayValue(curData)
        };
    }

    _debugReceive(component, channelRole, cnlNum, curData, commandFormat, parsedDate, error) {
        // Output diagnostic data to simplify channel binding troubleshooting.
        if (console?.debug) {
            console.debug("MimCalendar receive", {
                componentID: component.id,
                componentType: component.typeName,
                channelRole: channelRole,
                cnlNum: cnlNum,
                commandFormat: commandFormat || rs.mimic.CalendarCommandFormat.DOUBLE,
                parsedDate: parsedDate instanceof Date && !Number.isNaN(parsedDate.valueOf())
                    ? parsedDate.toISOString()
                    : null,
                error: error || "",
                ...this._getDebugData(curData)
            });
        }
    }

    _parseDateText(text) {
        text = String(text ?? "").trim();
        if (!text) {
            return null;
        }

        let dt = new Date(text);
        return Number.isNaN(dt.valueOf()) ? null : dt;
    }

    _parseDate(curData, commandFormat, hexOffset = 0) {
        // Read date from current data; command format only affects how commands are sent.
        if (!curData || curData.d.stat <= 0) {
            return null;
        }

        commandFormat = commandFormat || rs.mimic.CalendarCommandFormat.DOUBLE;

        // The server stores the current channel value as OADate double for all command formats.
        // Text and Hex affect only command transport, not the persisted current value.
        let rawVal = Number(curData.d.val);
        if (Number.isFinite(rawVal) && rawVal > 0) {
            return this._oaToDate(rawVal);
        }

        let dispVal = this._getDisplayValue(curData);
        if (dispVal) {
            if (commandFormat === rs.mimic.CalendarCommandFormat.HEX) {
                let val = this._doubleFromHex(dispVal, hexOffset);
                return Number.isFinite(val) && val > 0 ? this._oaToDate(val) : null;
            }

            return this._parseDateText(dispVal);
        }

        return null;
    }

    dataUpdated(args) {
        // Update the first date input from the bound input channel.
        let cnlNum = args.component.bindings?.inCnlNum;
        if (!(cnlNum > 0)) {
            return;
        }

        let curData = args.dataProvider.getCurData(cnlNum);
        let dt = this._parseDate(curData, args.component.properties.commandFormat);
        this._debugReceive(
            args.component,
            "Input channel 1",
            cnlNum,
            curData,
            args.component.properties.commandFormat,
            dt,
            dt ? "" : "Unable to parse date from channel data"
        );
        if (!dt) {
            return;
        }

        let value = this._dateToLocalInput(dt);
        if (args.component.properties.value1 !== value) {
            args.component.properties.value1 = value;
            args.propertyChanged = true;
        }
    }
};

rs.mimic.CalendarDoubleInputScript = class extends rs.mimic.CalendarSingleInputScript {
    dataUpdated(args) {
        super.dataUpdated(args);

        if (args.component.properties.commandFormat === rs.mimic.CalendarCommandFormat.HEX) {
            // In Hex mode the second date can be stored in a separate channel
            // or packed after the first date in the same hexadecimal value.
            let cnlNum2 = args.component.properties.secondInCnlNum;

            if (cnlNum2 > 0) {
                let curData2 = args.dataProvider.getCurData(cnlNum2);
                let dt2 = this._parseDate(curData2, args.component.properties.commandFormat);
                this._debugReceive(
                    args.component,
                    "Input channel 2",
                    cnlNum2,
                    curData2,
                    args.component.properties.commandFormat,
                    dt2,
                    dt2 ? "" : "Unable to parse date from channel data"
                );
                if (!dt2) {
                    return;
                }

                let value2 = this._dateToLocalInput(dt2);
                if (args.component.properties.value2 !== value2) {
                    args.component.properties.value2 = value2;
                    args.propertyChanged = true;
                }
                return;
            }

            let cnlNum = args.component.bindings?.inCnlNum;
            let curData = args.dataProvider.getCurData(cnlNum);
            let dt2 = this._parseDate(curData, args.component.properties.commandFormat, 8);
            this._debugReceive(
                args.component,
                "Input channel 2",
                cnlNum,
                curData,
                args.component.properties.commandFormat,
                dt2,
                dt2 ? "" : "Unable to parse second date from HEX channel data"
            );
            if (!dt2) {
                return;
            }

            let value2 = this._dateToLocalInput(dt2);
            if (args.component.properties.value2 !== value2) {
                args.component.properties.value2 = value2;
                args.propertyChanged = true;
            }
            return;
        }

        let cnlNum2 = args.component.properties.secondInCnlNum;
        if (!(cnlNum2 > 0)) {
            return;
        }

        let curData2 = args.dataProvider.getCurData(cnlNum2);
        let dt2 = this._parseDate(curData2, args.component.properties.commandFormat, 8);
        this._debugReceive(
            args.component,
            "Input channel 2",
            cnlNum2,
            curData2,
            args.component.properties.commandFormat,
            dt2,
            dt2 ? "" : "Unable to parse date from channel data"
        );
        if (!dt2) {
            return;
        }

        let value2 = this._dateToLocalInput(dt2);
        if (args.component.properties.value2 !== value2) {
            args.component.properties.value2 = value2;
            args.propertyChanged = true;
        }
    }
};

function getCalendarCommandFormat(sourceProps) {
    // Read the new command format property and migrate old "send as text" settings.
    const PropertyParser = rs.mimic.PropertyParser;
    const CalendarCommandFormat = rs.mimic.CalendarCommandFormat;
    let commandFormat = sourceProps?.commandFormat;

    if (commandFormat && typeof commandFormat === "object") {
        commandFormat = commandFormat.value ?? commandFormat.name ?? commandFormat.id ?? commandFormat.text;
    }

    commandFormat = PropertyParser.parseString(commandFormat).trim().toLowerCase();

    if (commandFormat === CalendarCommandFormat.TEXT.toLowerCase()) {
        return CalendarCommandFormat.TEXT;
    } else if (commandFormat === CalendarCommandFormat.HEX.toLowerCase()) {
        return CalendarCommandFormat.HEX;
    } else if (commandFormat === CalendarCommandFormat.DOUBLE.toLowerCase()) {
        return CalendarCommandFormat.DOUBLE;
    } else if (PropertyParser.parseBool(sourceProps?.sendAsText, false)) {
        return CalendarCommandFormat.TEXT;
    } else {
        return CalendarCommandFormat.DOUBLE;
    }
}

function setCalendarCommandFormat(props, sourceProps) {
    // Remove legacy behavior flags after mapping them to commandFormat.
    props.commandFormat = getCalendarCommandFormat(sourceProps);
    delete props.sendAsText;
    delete props.inputIsOADate;
    delete props.outputIsOADate;
}


rs.mimic.CalendarAutoFactory = class extends rs.mimic.RegularComponentFactory {
    _createExtraScript() {
        return new rs.mimic.CalendarSingleInputScript();
    }

    createProperties() {
        let props = super.createProperties();
        props.size.width = 372;
        props.size.height = 44;
        props.label = "Date and time";
        props.labelWidth = 160;
        props.labelForeColor = "";
        props.labelBackColor = "";
        props.labelFont = new rs.mimic.Font({ inherit: true });
        props.inputWidth = 198;
        props.inputForeColor = "";
        props.inputBackColor = "";
        props.inputFont = new rs.mimic.Font({ inherit: true });
        props.commandFormat = rs.mimic.CalendarCommandFormat.DOUBLE;
        props.autoSend = true;
        props.value1 = "";
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.label = PropertyParser.parseString(sourceProps.label);
        props.labelWidth = PropertyParser.parseInt(sourceProps.labelWidth, 160);
        props.labelHeight = PropertyParser.parseInt(sourceProps.labelHeight, 32);
        props.labelForeColor = PropertyParser.parseString(sourceProps.labelForeColor);
        props.labelBackColor = PropertyParser.parseString(sourceProps.labelBackColor);
        props.labelFont = rs.mimic.Font.parse(sourceProps.labelFont);
        props.inputWidth = PropertyParser.parseInt(sourceProps.inputWidth, 198);
        props.inputHeight = PropertyParser.parseInt(sourceProps.inputHeight, 32);
        props.inputForeColor = PropertyParser.parseString(sourceProps.inputForeColor);
        props.inputBackColor = PropertyParser.parseString(sourceProps.inputBackColor);
        props.inputFont = rs.mimic.Font.parse(sourceProps.inputFont);
        setCalendarCommandFormat(props, sourceProps);
        props.autoSend = PropertyParser.parseBool(sourceProps.autoSend, true);
        props.value1 = PropertyParser.parseString(sourceProps.value1);
        return props;
    }

    createComponent() {
        return super.createComponent("CalendarAuto");
    }
};

rs.mimic.CalendarInputFactory = class extends rs.mimic.RegularComponentFactory {
    _createExtraScript() {
        return new rs.mimic.CalendarSingleInputScript();
    }

    createProperties() {
        let props = super.createProperties();
        props.size.width = 198;
        props.size.height = 32;
        props.inputWidth = 198;
        props.inputHeight = 32;
        props.inputForeColor = "";
        props.inputBackColor = "";
        props.inputFont = new rs.mimic.Font({ inherit: true });
        props.commandFormat = rs.mimic.CalendarCommandFormat.DOUBLE;
        props.autoSend = true;
        props.value1 = "";
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.inputWidth = PropertyParser.parseInt(sourceProps.inputWidth, 198);
        props.inputHeight = PropertyParser.parseInt(sourceProps.inputHeight, 32);
        props.inputForeColor = PropertyParser.parseString(sourceProps.inputForeColor);
        props.inputBackColor = PropertyParser.parseString(sourceProps.inputBackColor);
        props.inputFont = rs.mimic.Font.parse(sourceProps.inputFont);
        setCalendarCommandFormat(props, sourceProps);
        props.autoSend = PropertyParser.parseBool(sourceProps.autoSend, true);
        props.value1 = PropertyParser.parseString(sourceProps.value1);
        return props;
    }

    createComponent() {
        return super.createComponent("CalendarInput");
    }
};

rs.mimic.CalendarButtonFactory = class extends rs.mimic.RegularComponentFactory {
    _createExtraScript() {
        return new rs.mimic.CalendarSingleInputScript();
    }

    createProperties() {
        let props = super.createProperties();
        props.size.width = 494;
        props.size.height = 44;
        props.label = "Date and time";
        props.buttonText = "Set";
        props.labelWidth = 160;
        props.labelHeight = 32;
        props.labelForeColor = "";
        props.labelBackColor = "";
        props.labelFont = new rs.mimic.Font({ inherit: true });
        props.inputWidth = 198;
        props.inputHeight = 32;
        props.inputForeColor = "";
        props.inputBackColor = "";
        props.inputFont = new rs.mimic.Font({ inherit: true });
        props.btnWidth = 110;
        props.btnHeight = 32;
        props.buttonForeColor = "";
        props.buttonBackColor = "";
        props.buttonFont = new rs.mimic.Font({ inherit: true });
        props.buttonCornerRadius = new rs.mimic.CornerRadius();
        props.commandFormat = rs.mimic.CalendarCommandFormat.DOUBLE;
        props.value1 = "";
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.label = PropertyParser.parseString(sourceProps.label);
        props.buttonText = PropertyParser.parseString(sourceProps.buttonText);
        props.labelWidth = PropertyParser.parseInt(sourceProps.labelWidth, 160);
        props.labelHeight = PropertyParser.parseInt(sourceProps.labelHeight, 32);
        props.labelForeColor = PropertyParser.parseString(sourceProps.labelForeColor);
        props.labelBackColor = PropertyParser.parseString(sourceProps.labelBackColor);
        props.labelFont = rs.mimic.Font.parse(sourceProps.labelFont);
        props.inputWidth = PropertyParser.parseInt(sourceProps.inputWidth, 198);
        props.inputHeight = PropertyParser.parseInt(sourceProps.inputHeight, 32);
        props.inputForeColor = PropertyParser.parseString(sourceProps.inputForeColor);
        props.inputBackColor = PropertyParser.parseString(sourceProps.inputBackColor);
        props.inputFont = rs.mimic.Font.parse(sourceProps.inputFont);
        props.btnWidth = PropertyParser.parseInt(sourceProps.btnWidth, 110);
        props.btnHeight = PropertyParser.parseInt(sourceProps.btnHeight, 32);
        props.buttonForeColor = PropertyParser.parseString(sourceProps.buttonForeColor);
        props.buttonBackColor = PropertyParser.parseString(sourceProps.buttonBackColor);
        props.buttonFont = rs.mimic.Font.parse(sourceProps.buttonFont);
        props.buttonCornerRadius = rs.mimic.CornerRadius.parse(sourceProps.buttonCornerRadius);
        setCalendarCommandFormat(props, sourceProps);
        props.value1 = PropertyParser.parseString(sourceProps.value1);
        return props;
    }

    createComponent() {
        return super.createComponent("CalendarButton");
    }
};

// 1 calendar + button on the next row
rs.mimic.CalendarRangeFactory = class extends rs.mimic.RegularComponentFactory {
    _createExtraScript() {
        return new rs.mimic.CalendarSingleInputScript();
    }

    createProperties() {
        let props = super.createProperties();
        props.size.width = 377;
        props.size.height = 82;
        props.label = "Date and time";
        props.buttonText = "Set";
        props.labelWidth = 160;
        props.labelHeight = 32;
        props.labelForeColor = "";
        props.labelBackColor = "";
        props.labelFont = new rs.mimic.Font({ inherit: true });
        props.inputWidth = 198;
        props.inputHeight = 32;
        props.inputForeColor = "";
        props.inputBackColor = "";
        props.inputFont = new rs.mimic.Font({ inherit: true });
        props.btnWidth = 198;
        props.btnHeight = 32;
        props.buttonForeColor = "";
        props.buttonBackColor = "";
        props.buttonFont = new rs.mimic.Font({ inherit: true });
        props.buttonCornerRadius = new rs.mimic.CornerRadius();
        props.commandFormat = rs.mimic.CalendarCommandFormat.DOUBLE;
        props.value1 = "";
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.label = PropertyParser.parseString(sourceProps.label);
        props.buttonText = PropertyParser.parseString(sourceProps.buttonText);
        props.labelWidth = PropertyParser.parseInt(sourceProps.labelWidth, 160);
        props.labelHeight = PropertyParser.parseInt(sourceProps.labelHeight, 32);
        props.labelForeColor = PropertyParser.parseString(sourceProps.labelForeColor);
        props.labelBackColor = PropertyParser.parseString(sourceProps.labelBackColor);
        props.labelFont = rs.mimic.Font.parse(sourceProps.labelFont);
        props.inputWidth = PropertyParser.parseInt(sourceProps.inputWidth, 198);
        props.inputHeight = PropertyParser.parseInt(sourceProps.inputHeight, 32);
        props.inputForeColor = PropertyParser.parseString(sourceProps.inputForeColor);
        props.inputBackColor = PropertyParser.parseString(sourceProps.inputBackColor);
        props.inputFont = rs.mimic.Font.parse(sourceProps.inputFont);
        props.btnWidth = PropertyParser.parseInt(sourceProps.btnWidth, 198);
        props.btnHeight = PropertyParser.parseInt(sourceProps.btnHeight, 32);
        props.buttonForeColor = PropertyParser.parseString(sourceProps.buttonForeColor);
        props.buttonBackColor = PropertyParser.parseString(sourceProps.buttonBackColor);
        props.buttonFont = rs.mimic.Font.parse(sourceProps.buttonFont);
        props.buttonCornerRadius = rs.mimic.CornerRadius.parse(sourceProps.buttonCornerRadius);
        setCalendarCommandFormat(props, sourceProps);
        props.value1 = PropertyParser.parseString(sourceProps.value1);
        return props;
    }

    createComponent() {
        return super.createComponent("CalendarRange");
    }
};

rs.mimic.CalendarDoubleRangeFactory = class extends rs.mimic.RegularComponentFactory {
    _createExtraScript() {
        return new rs.mimic.CalendarDoubleInputScript();
    }

    createProperties() {
        let props = super.createProperties();
        props.size.width = 490;
        props.size.height = 82;
        props.labelFrom = "Date and time 1";
        props.labelTo = "Date and time 2";
        props.buttonText = "Set";
        props.secondInCnlNum = 0;
        props.secondOutCnlNum = 0;
        props.labelWidth = 160;
        props.labelHeight = 32;
        props.labelForeColor = "";
        props.labelBackColor = "";
        props.labelFont = new rs.mimic.Font({ inherit: true });
        props.inputWidth = 198;
        props.inputHeight = 32;
        props.inputForeColor = "";
        props.inputBackColor = "";
        props.inputFont = new rs.mimic.Font({ inherit: true });
        props.btnWidth = 110;
        props.btnHeight = 32;
        props.buttonForeColor = "";
        props.buttonBackColor = "";
        props.buttonFont = new rs.mimic.Font({ inherit: true });
        props.buttonCornerRadius = new rs.mimic.CornerRadius();
        props.commandFormat = rs.mimic.CalendarCommandFormat.DOUBLE;
        props.value1 = "";
        props.value2 = "";
        return props;
    }

    parseProperties(sourceProps) {
        const PropertyParser = rs.mimic.PropertyParser;
        let props = super.parseProperties(sourceProps);
        sourceProps ??= {};
        props.labelFrom = PropertyParser.parseString(sourceProps.labelFrom);
        props.labelTo = PropertyParser.parseString(sourceProps.labelTo);
        props.buttonText = PropertyParser.parseString(sourceProps.buttonText);
        props.secondInCnlNum = PropertyParser.parseInt(sourceProps.secondInCnlNum);
        props.secondOutCnlNum = PropertyParser.parseInt(sourceProps.secondOutCnlNum);
        props.labelWidth = PropertyParser.parseInt(sourceProps.labelWidth, 160);
        props.labelHeight = PropertyParser.parseInt(sourceProps.labelHeight, 32);
        props.labelForeColor = PropertyParser.parseString(sourceProps.labelForeColor);
        props.labelBackColor = PropertyParser.parseString(sourceProps.labelBackColor);
        props.labelFont = rs.mimic.Font.parse(sourceProps.labelFont);
        props.inputWidth = PropertyParser.parseInt(sourceProps.inputWidth, 198);
        props.inputHeight = PropertyParser.parseInt(sourceProps.inputHeight, 32);
        props.inputForeColor = PropertyParser.parseString(sourceProps.inputForeColor);
        props.inputBackColor = PropertyParser.parseString(sourceProps.inputBackColor);
        props.inputFont = rs.mimic.Font.parse(sourceProps.inputFont);
        props.btnWidth = PropertyParser.parseInt(sourceProps.btnWidth, 110);
        props.btnHeight = PropertyParser.parseInt(sourceProps.btnHeight, 32);
        props.buttonForeColor = PropertyParser.parseString(sourceProps.buttonForeColor);
        props.buttonBackColor = PropertyParser.parseString(sourceProps.buttonBackColor);
        props.buttonFont = rs.mimic.Font.parse(sourceProps.buttonFont);
        props.buttonCornerRadius = rs.mimic.CornerRadius.parse(sourceProps.buttonCornerRadius);
        setCalendarCommandFormat(props, sourceProps);
        props.value1 = PropertyParser.parseString(sourceProps.value1);
        props.value2 = PropertyParser.parseString(sourceProps.value2);
        return props;
    }
};

rs.mimic.CalendarRangeBottomFactory = class extends rs.mimic.CalendarDoubleRangeFactory {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 377;
        props.size.height = 120;
        props.btnWidth = 198;
        return props;
    }

    createComponent() {
        return rs.mimic.RegularComponentFactory.prototype.createComponent.call(this, "CalendarRangeBottom");
    }
};

rs.mimic.CalendarRangeSideFactory = class extends rs.mimic.CalendarDoubleRangeFactory {
    createProperties() {
        let props = super.createProperties();
        props.size.width = 490;
        props.size.height = 82;
        return props;
    }

    createComponent() {
        return rs.mimic.RegularComponentFactory.prototype.createComponent.call(this, "CalendarRangeSide");
    }
};


function registerCalendarFactories() {
    let componentFactories = rs.mimic.FactorySet.componentFactories;
    componentFactories.set("CalendarAuto", new rs.mimic.CalendarAutoFactory());
    componentFactories.set("CalendarInput", new rs.mimic.CalendarInputFactory());
    componentFactories.set("CalendarButton", new rs.mimic.CalendarButtonFactory());
    componentFactories.set("CalendarRange", new rs.mimic.CalendarRangeFactory());
    componentFactories.set("CalendarRangeBottom", new rs.mimic.CalendarRangeBottomFactory());
    componentFactories.set("CalendarRangeSide", new rs.mimic.CalendarRangeSideFactory());
}

registerCalendarFactories();
