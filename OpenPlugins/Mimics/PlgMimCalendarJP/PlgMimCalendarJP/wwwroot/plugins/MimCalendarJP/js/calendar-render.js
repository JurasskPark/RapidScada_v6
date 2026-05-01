// Contains renderers for calendar components.

rs.mimic.CalendarRendererBase = class extends rs.mimic.RegularComponentRenderer {
    _applyCssVars(componentElem, props) {
        // Set CSS custom properties on the root element so the stylesheet can use them.
        if (props.labelWidth > 0) componentElem.css("--label-width", props.labelWidth + "px");
        if (props.labelHeight > 0) componentElem.css("--label-height", props.labelHeight + "px");
        if (props.inputWidth > 0) componentElem.css("--input-width", props.inputWidth + "px");
        if (props.inputHeight > 0) componentElem.css("--control-height", props.inputHeight + "px");
        if (props.btnWidth > 0) componentElem.css("--btn-width", props.btnWidth + "px");
        if (props.btnHeight > 0) componentElem.css("--btn-height", props.btnHeight + "px");
    }

    _applyLabelStyle(labelElem, props, renderContext) {
        // Apply label style properties configured in the mimic editor.
        labelElem.css({
            "color": props.labelForeColor || "",
            "background-color": props.labelBackColor || ""
        });
        this._setFont(labelElem, props.labelFont, renderContext.fontMap);
    }

    _applyButtonStyle(buttonElem, props, renderContext) {
        // Apply button style properties configured in the mimic editor.
        buttonElem.css({
            "color": props.buttonForeColor || "",
            "background-color": props.buttonBackColor || ""
        });
        this._setFont(buttonElem, props.buttonFont, renderContext.fontMap);
        this._setCornerRadius(buttonElem, props.buttonCornerRadius);
    }

    _applyInputStyle(inputElem, props, renderContext) {
        // Apply date input style properties configured in the mimic editor.
        inputElem.css({
            "color": props.inputForeColor || "",
            "background-color": props.inputBackColor || ""
        });
        this._setFont(inputElem, props.inputFont, renderContext.fontMap);
    }

    _dateToOADate(date) {
        // Convert JavaScript Date to OLE Automation date used by Rapid SCADA.
        return date.getTime() / 86400000 + 25569;
    }

    _dateToHex(date) {
        // Encode one OADate value as little-endian hexadecimal command data.
        let bytes = new Uint8Array(8);
        new DataView(bytes.buffer).setFloat64(0, this._dateToOADate(date), true);
        return Array.from(bytes, b => b.toString(16).padStart(2, "0")).join("").toUpperCase();
    }

    _sendCommand(renderContext, cnlNum, cmdVal, isHex, cmdData) {
        // Send command through the standard mimic API and log failures to the browser console.
        renderContext.mainApi.sendCommand(cnlNum, cmdVal, isHex, cmdData, dto => {
            if (!dto?.ok) {
                console.error("Calendar command failed.", {
                    cnlNum: cnlNum,
                    cmdVal: cmdVal,
                    isHex: isHex,
                    cmdData: cmdData,
                    message: dto?.msg || ""
                });
            }
        });
    }

    _toLocalDateTimeValue(date) {
        const pad = n => String(n).padStart(2, "0");
        return date.getFullYear() + "-" + pad(date.getMonth() + 1) + "-" + pad(date.getDate()) + "T" +
            pad(date.getHours()) + ":" + pad(date.getMinutes());
    }

    _readDateValue(inputElem) {
        const value = inputElem.val();
        return value ? new Date(value) : null;
    }

    _sendDateTime(renderContext, cnlNum, date, commandFormat) {
        // Send a single date using the selected transport format.
        if (!renderContext.mainApi || !Number.isFinite(cnlNum) || cnlNum <= 0 || !(date instanceof Date) || Number.isNaN(date.valueOf())) {
            return;
        }

        commandFormat = commandFormat || rs.mimic.CalendarCommandFormat.DOUBLE;

        if (commandFormat === rs.mimic.CalendarCommandFormat.TEXT) {
            this._sendCommand(renderContext, cnlNum, 0, false, date.toISOString());
        } else if (commandFormat === rs.mimic.CalendarCommandFormat.HEX) {
            this._sendCommand(renderContext, cnlNum, 0, true, this._dateToHex(date));
        } else {
            this._sendCommand(renderContext, cnlNum, this._dateToOADate(date), false, null);
        }
    }

    _sendDateTimeHex(renderContext, cnlNum, dates) {
        // Send several date values as one hexadecimal command.
        if (!renderContext.mainApi || !Number.isFinite(cnlNum) || cnlNum <= 0) {
            return;
        }

        let hex = "";
        for (let date of dates) {
            if (!(date instanceof Date) || Number.isNaN(date.valueOf())) {
                return;
            }
            hex += this._dateToHex(date);
        }

        this._sendCommand(renderContext, cnlNum, 0, true, hex);
    }

    _getEffectiveCnlNum(component, propName) {
        // Inherit channel number from parent component (faceplate) if not set on this component.
        let cnlNum = component.properties[propName];
        if (cnlNum > 0) return cnlNum;
        let parent = component.parent;
        while (parent) {
            if (parent.properties && parent.properties[propName] > 0) {
                return parent.properties[propName];
            }
            parent = parent.parent;
        }
        return 0;
    }

    _sendOneValue(component, renderContext, inputElem) {
        let props = component.properties;
        let dt = this._readDateValue(inputElem);
        let outCnlNum = this._getEffectiveCnlNum(component, "outCnlNum");
        this._sendDateTime(renderContext, outCnlNum, dt, props.commandFormat);
        props.value1 = inputElem.val() || "";
    }

    _sendTwoValues(component, renderContext, inputElem1, inputElem2) {
        // In Hex mode both dates are packed into one command; other formats use separate commands.
        let props = component.properties;
        let dt1 = this._readDateValue(inputElem1);
        let dt2 = this._readDateValue(inputElem2);
        let outCnlNum = this._getEffectiveCnlNum(component, "outCnlNum");

        if (props.commandFormat === rs.mimic.CalendarCommandFormat.HEX) {
            this._sendDateTimeHex(renderContext, outCnlNum, [dt1, dt2]);
        } else {
            this._sendDateTime(renderContext, outCnlNum, dt1, props.commandFormat);
            this._sendDateTime(renderContext, props.secondOutCnlNum, dt2, props.commandFormat);
        }

        props.value1 = inputElem1.val() || "";
        props.value2 = inputElem2.val() || "";
    }

    _setInputValue(inputElem, defaultValue) {
        // Show saved value if available, otherwise initialize the input with the current time.
        if (defaultValue) {
            inputElem.val(defaultValue);
        } else if (!inputElem.val()) {
            inputElem.val(this._toLocalDateTimeValue(new Date()));
        }
    }

    _setBaseProps(componentElem, component, renderContext) {
        super._setProps(componentElem, component, renderContext);
        this._applyCssVars(componentElem, component.properties);
    }
};

rs.mimic.CalendarAutoRenderer = class extends rs.mimic.CalendarRendererBase {
    _completeDom(componentElem) {
        componentElem.append("<div class='calendar-row'><label class='calendar-label'></label><input type='datetime-local' class='calendar-dt' /></div>");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("calendar-comp calendar-auto");
    }

    _setProps(componentElem, component, renderContext) {
        this._setBaseProps(componentElem, component, renderContext);
        let props = component.properties;
        let labelElem = componentElem.find("label.calendar-label:first");
        labelElem.text(props.label || "");
        this._applyLabelStyle(labelElem, props, renderContext);
        let inputElem = componentElem.find("input.calendar-dt:first");
        this._applyInputStyle(inputElem, props, renderContext);
        this._setInputValue(inputElem, props.value1);
    }

    _bindEvents(componentElem, component, renderContext) {
        super._bindEvents(componentElem, component, renderContext);
        componentElem.off("change.rs.calendar");

        if (!renderContext.editMode && component.properties.autoSend) {
            componentElem.on("change.rs.calendar", "input.calendar-dt", () => {
                this._sendOneValue(component, renderContext, componentElem.find("input.calendar-dt:first"));
            });
        }
    }
};

rs.mimic.CalendarInputRenderer = class extends rs.mimic.CalendarRendererBase {
    _completeDom(componentElem) {
        componentElem.append("<input type='datetime-local' class='calendar-dt' />");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("calendar-comp calendar-input");
    }

    _setProps(componentElem, component, renderContext) {
        this._setBaseProps(componentElem, component, renderContext);
        let props = component.properties;
        let inputElem = componentElem.find("input.calendar-dt:first");
        this._applyInputStyle(inputElem, props, renderContext);
        this._setInputValue(inputElem, props.value1);
    }

    _bindEvents(componentElem, component, renderContext) {
        super._bindEvents(componentElem, component, renderContext);
        componentElem.off("change.rs.calendar");

        if (!renderContext.editMode && component.properties.autoSend) {
            componentElem.on("change.rs.calendar", "input.calendar-dt", () => {
                this._sendOneValue(component, renderContext, componentElem.find("input.calendar-dt:first"));
            });
        }
    }
};

rs.mimic.CalendarButtonRenderer = class extends rs.mimic.CalendarRendererBase {
    _completeDom(componentElem) {
        componentElem.append("<div class='calendar-row'><label class='calendar-label'></label><input type='datetime-local' class='calendar-dt' /><button type='button' class='calendar-set-btn'></button></div>");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("calendar-comp calendar-button");
    }

    _setProps(componentElem, component, renderContext) {
        this._setBaseProps(componentElem, component, renderContext);
        let props = component.properties;
        let labelElem = componentElem.find("label.calendar-label:first");
        let btnElem = componentElem.find("button.calendar-set-btn:first");
        labelElem.text(props.label || "");
        btnElem.text(props.buttonText || "Set");
        this._applyLabelStyle(labelElem, props, renderContext);
        let inputElem = componentElem.find("input.calendar-dt:first");
        this._applyInputStyle(inputElem, props, renderContext);
        this._applyButtonStyle(btnElem, props, renderContext);
        this._setInputValue(inputElem, props.value1);
    }

    _bindEvents(componentElem, component, renderContext) {
        super._bindEvents(componentElem, component, renderContext);
        componentElem.off("click.rs.calendar");

        if (!renderContext.editMode) {
            componentElem.on("click.rs.calendar", "button.calendar-set-btn", () => {
                this._sendOneValue(component, renderContext, componentElem.find("input.calendar-dt:first"));
            });
        }
    }
};

rs.mimic.CalendarRangeBaseRenderer = class extends rs.mimic.CalendarRendererBase {
    _setCommonRangeProps(componentElem, props) {
        componentElem.find("label.calendar-label-1:first").text(props.labelFrom || "");
        componentElem.find("label.calendar-label-2:first").text(props.labelTo || "");
        componentElem.find("button.calendar-set-btn:first").text(props.buttonText || "Set");
        this._setInputValue(componentElem.find("input.calendar-dt-1:first"), props.value1);
        this._setInputValue(componentElem.find("input.calendar-dt-2:first"), props.value2);
    }

    _applyCommonRangeInputStyle(componentElem, props, renderContext) {
        this._applyInputStyle(componentElem.find("input.calendar-dt-1:first"), props, renderContext);
        this._applyInputStyle(componentElem.find("input.calendar-dt-2:first"), props, renderContext);
    }

    _bindRangeButton(componentElem, component, renderContext) {
        componentElem.off("click.rs.calendar");

        if (!renderContext.editMode) {
            componentElem.on("click.rs.calendar", "button.calendar-set-btn", () => {
                this._sendTwoValues(
                    component,
                    renderContext,
                    componentElem.find("input.calendar-dt-1:first"),
                    componentElem.find("input.calendar-dt-2:first")
                );
            });
        }
    }
};

rs.mimic.CalendarRangeRenderer = class extends rs.mimic.CalendarRendererBase {
    _completeDom(componentElem) {
        componentElem.append("<div class='calendar-row'><label class='calendar-label'></label><input type='datetime-local' class='calendar-dt' /></div>" +
            "<div class='calendar-row calendar-row-action'><div class='calendar-label calendar-label-spacer'></div><button type='button' class='calendar-set-btn'></button></div>");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("calendar-comp calendar-range");
    }

    _setProps(componentElem, component, renderContext) {
        this._setBaseProps(componentElem, component, renderContext);
        let props = component.properties;
        let labelElem = componentElem.find("label.calendar-label:first");
        let btnElem = componentElem.find("button.calendar-set-btn:first");
        labelElem.text(props.label || "");
        btnElem.text(props.buttonText || "Set");
        this._applyLabelStyle(labelElem, props, renderContext);
        let inputElem = componentElem.find("input.calendar-dt:first");
        this._applyInputStyle(inputElem, props, renderContext);
        this._applyButtonStyle(btnElem, props, renderContext);
        this._setInputValue(inputElem, props.value1);
    }

    _bindEvents(componentElem, component, renderContext) {
        super._bindEvents(componentElem, component, renderContext);
        componentElem.off("click.rs.calendar");

        if (!renderContext.editMode) {
            componentElem.on("click.rs.calendar", "button.calendar-set-btn", () => {
                this._sendOneValue(component, renderContext, componentElem.find("input.calendar-dt:first"));
            });
        }
    }
};

rs.mimic.CalendarRangeBottomRenderer = class extends rs.mimic.CalendarRangeBaseRenderer {
    _completeDom(componentElem) {
        componentElem.append("<div class='calendar-row'><label class='calendar-label calendar-label-1'></label><input type='datetime-local' class='calendar-dt-1' /></div>" +
            "<div class='calendar-row'><label class='calendar-label calendar-label-2'></label><input type='datetime-local' class='calendar-dt-2' /></div>" +
            "<div class='calendar-row calendar-row-action'><div class='calendar-label calendar-label-spacer'></div><button type='button' class='calendar-set-btn'></button></div>");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("calendar-comp calendar-range-bottom");
    }

    _setProps(componentElem, component, renderContext) {
        this._setBaseProps(componentElem, component, renderContext);
        let props = component.properties;
        this._setCommonRangeProps(componentElem, props);
        this._applyLabelStyle(componentElem.find("label.calendar-label-1:first"), props, renderContext);
        this._applyLabelStyle(componentElem.find("label.calendar-label-2:first"), props, renderContext);
        this._applyCommonRangeInputStyle(componentElem, props, renderContext);
        this._applyButtonStyle(componentElem.find("button.calendar-set-btn:first"), props, renderContext);
    }

    _bindEvents(componentElem, component, renderContext) {
        super._bindEvents(componentElem, component, renderContext);
        this._bindRangeButton(componentElem, component, renderContext);
    }
};

rs.mimic.CalendarRangeSideRenderer = class extends rs.mimic.CalendarRangeBaseRenderer {
    _completeDom(componentElem) {
        componentElem.append("<div class='calendar-grid-left'>" +
            "<div class='calendar-row'><label class='calendar-label calendar-label-1'></label><input type='datetime-local' class='calendar-dt-1' /></div>" +
            "<div class='calendar-row'><label class='calendar-label calendar-label-2'></label><input type='datetime-local' class='calendar-dt-2' /></div>" +
            "</div><div class='calendar-grid-right'><button type='button' class='calendar-set-btn'></button></div>");
    }

    _setClasses(componentElem, component, renderContext) {
        super._setClasses(componentElem, component, renderContext);
        componentElem.addClass("calendar-comp calendar-range-side");
    }

    _setProps(componentElem, component, renderContext) {
        this._setBaseProps(componentElem, component, renderContext);
        let props = component.properties;
        this._setCommonRangeProps(componentElem, props);
        this._applyLabelStyle(componentElem.find("label.calendar-label-1:first"), props, renderContext);
        this._applyLabelStyle(componentElem.find("label.calendar-label-2:first"), props, renderContext);
        this._applyCommonRangeInputStyle(componentElem, props, renderContext);
        this._applyButtonStyle(componentElem.find("button.calendar-set-btn:first"), props, renderContext);
    }

    _bindEvents(componentElem, component, renderContext) {
        super._bindEvents(componentElem, component, renderContext);
        this._bindRangeButton(componentElem, component, renderContext);
    }
};


function registerCalendarRenderers() {
    let componentRenderers = rs.mimic.RendererSet.componentRenderers;
    componentRenderers.set("CalendarAuto", new rs.mimic.CalendarAutoRenderer());
    componentRenderers.set("CalendarInput", new rs.mimic.CalendarInputRenderer());
    componentRenderers.set("CalendarButton", new rs.mimic.CalendarButtonRenderer());
    componentRenderers.set("CalendarRange", new rs.mimic.CalendarRangeRenderer());
    componentRenderers.set("CalendarRangeBottom", new rs.mimic.CalendarRangeBottomRenderer());
    componentRenderers.set("CalendarRangeSide", new rs.mimic.CalendarRangeSideRenderer());
}

registerCalendarRenderers();
