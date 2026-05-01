// Contains property descriptors for calendar components.

function configureCalendarBaseDescriptor(descriptor) {
    // Hide inherited properties that are not used by calendar components.
    const hiddenProps = [
        "blinking",
        "clickAction",
        "checkRights",
        "deviceNum",
        "inputIsOADate",
        "objNum",
        "outputIsOADate",
        "sendAsText",
        "propertyBindings"
    ];

    for (let propName of hiddenProps) {
        let propDescr = descriptor.get(propName);
        if (propDescr) {
            propDescr.isBrowsable = false;
        }
    }
}

function addLabelStyleProperties(descriptor) {
    // Add common style properties for labels.
    const KnownCategory = rs.mimic.KnownCategory;
    const BasicType = rs.mimic.BasicType;
    const Subtype = rs.mimic.Subtype;
    const PropertyEditor = rs.mimic.PropertyEditor;
    const PropertyDescriptor = rs.mimic.PropertyDescriptor;

    descriptor.add(new PropertyDescriptor({
        name: "labelWidth",
        displayName: "Label width",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));
    descriptor.add(new PropertyDescriptor({
        name: "labelHeight",
        displayName: "Label height",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));
    descriptor.add(new PropertyDescriptor({
        name: "labelForeColor",
        displayName: "Label text color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "labelBackColor",
        displayName: "Label back color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "labelFont",
        displayName: "Label font",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRUCT,
        subtype: Subtype.FONT,
        editor: PropertyEditor.FONT_DIALOG
    }));
}

function addButtonStyleProperties(descriptor) {
    // Add common style properties for buttons.
    const KnownCategory = rs.mimic.KnownCategory;
    const BasicType = rs.mimic.BasicType;
    const Subtype = rs.mimic.Subtype;
    const PropertyEditor = rs.mimic.PropertyEditor;
    const PropertyDescriptor = rs.mimic.PropertyDescriptor;

    descriptor.add(new PropertyDescriptor({
        name: "btnWidth",
        displayName: "Button width",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));
    descriptor.add(new PropertyDescriptor({
        name: "btnHeight",
        displayName: "Button height",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));
    descriptor.add(new PropertyDescriptor({
        name: "buttonForeColor",
        displayName: "Button text color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "buttonBackColor",
        displayName: "Button back color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "buttonFont",
        displayName: "Button font",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRUCT,
        subtype: Subtype.FONT,
        editor: PropertyEditor.FONT_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "buttonCornerRadius",
        displayName: "Button corner radius",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRUCT,
        subtype: Subtype.CORNER_RADIUS
    }));
}

function addInputStyleProperties(descriptor) {
    // Add common style properties for date inputs.
    const KnownCategory = rs.mimic.KnownCategory;
    const BasicType = rs.mimic.BasicType;
    const Subtype = rs.mimic.Subtype;
    const PropertyEditor = rs.mimic.PropertyEditor;
    const PropertyDescriptor = rs.mimic.PropertyDescriptor;

    descriptor.add(new PropertyDescriptor({
        name: "inputWidth",
        displayName: "Date input width",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));
    descriptor.add(new PropertyDescriptor({
        name: "inputHeight",
        displayName: "Date input height",
        category: KnownCategory.APPEARANCE,
        type: BasicType.INT
    }));
    descriptor.add(new PropertyDescriptor({
        name: "inputForeColor",
        displayName: "Date text color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "inputBackColor",
        displayName: "Date back color",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRING,
        editor: PropertyEditor.COLOR_DIALOG
    }));
    descriptor.add(new PropertyDescriptor({
        name: "inputFont",
        displayName: "Date font",
        category: KnownCategory.APPEARANCE,
        type: BasicType.STRUCT,
        subtype: Subtype.FONT,
        editor: PropertyEditor.FONT_DIALOG
    }));
}

function addCommandFormatProperty(descriptor) {
    // Select how commands are sent to output channels.
    const KnownCategory = rs.mimic.KnownCategory;
    const BasicType = rs.mimic.BasicType;
    const CalendarCommandFormat = rs.mimic.CalendarCommandFormat;
    const PropertyDescriptor = rs.mimic.PropertyDescriptor;

    descriptor.add(new PropertyDescriptor({
        name: "commandFormat",
        displayName: "Command format",
        category: KnownCategory.BEHAVIOR,
        type: BasicType.ENUM,
        subtype: "CalendarCommandFormat",
        tweakpaneOptions: {
            options: {
                Double: CalendarCommandFormat.DOUBLE,
                Text: CalendarCommandFormat.TEXT,
                Hex: CalendarCommandFormat.HEX
            }
        }
    }));
}

rs.mimic.CalendarAutoDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureCalendarBaseDescriptor(this);

        this.add(new PropertyDescriptor({ name: "inCnlNum", displayName: "Input channel 1", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "outCnlNum", displayName: "Output channel 1", category: KnownCategory.DATA, type: BasicType.INT }));

        this.add(new PropertyDescriptor({ name: "label", displayName: "Label", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        addLabelStyleProperties(this);
        addInputStyleProperties(this);
        addCommandFormatProperty(this);
        this.add(new PropertyDescriptor({ name: "autoSend", displayName: "Auto send", category: KnownCategory.BEHAVIOR, type: BasicType.BOOL }));
        this.add(new PropertyDescriptor({ name: "value1", displayName: "Value 1", category: KnownCategory.MISC, isBrowsable: false, type: BasicType.STRING }));
    }
};

rs.mimic.CalendarInputDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureCalendarBaseDescriptor(this);

        this.add(new PropertyDescriptor({ name: "inCnlNum", displayName: "Input channel 1", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "outCnlNum", displayName: "Output channel 1", category: KnownCategory.DATA, type: BasicType.INT }));

        addInputStyleProperties(this);
        addCommandFormatProperty(this);
        this.add(new PropertyDescriptor({ name: "autoSend", displayName: "Auto send", category: KnownCategory.BEHAVIOR, type: BasicType.BOOL }));
        this.add(new PropertyDescriptor({ name: "value1", displayName: "Value 1", category: KnownCategory.MISC, isBrowsable: false, type: BasicType.STRING }));
    }
};

rs.mimic.CalendarButtonDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureCalendarBaseDescriptor(this);

        this.add(new PropertyDescriptor({ name: "inCnlNum", displayName: "Input channel 1", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "outCnlNum", displayName: "Output channel 1", category: KnownCategory.DATA, type: BasicType.INT }));

        this.add(new PropertyDescriptor({ name: "label", displayName: "Label", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        this.add(new PropertyDescriptor({ name: "buttonText", displayName: "Button text", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        addLabelStyleProperties(this);
        addInputStyleProperties(this);
        addButtonStyleProperties(this);
        addCommandFormatProperty(this);
        this.add(new PropertyDescriptor({ name: "value1", displayName: "Value 1", category: KnownCategory.MISC, isBrowsable: false, type: BasicType.STRING }));
    }
};

rs.mimic.CalendarRangeDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureCalendarBaseDescriptor(this);

        this.add(new PropertyDescriptor({ name: "inCnlNum", displayName: "Input channel 1", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "outCnlNum", displayName: "Output channel 1", category: KnownCategory.DATA, type: BasicType.INT }));

        this.add(new PropertyDescriptor({ name: "label", displayName: "Label", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        this.add(new PropertyDescriptor({ name: "buttonText", displayName: "Button text", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        addLabelStyleProperties(this);
        addInputStyleProperties(this);
        addButtonStyleProperties(this);
        addCommandFormatProperty(this);
        this.add(new PropertyDescriptor({ name: "value1", displayName: "Value 1", category: KnownCategory.MISC, isBrowsable: false, type: BasicType.STRING }));
    }
};

rs.mimic.CalendarDoubleRangeDescriptor = class extends rs.mimic.RegularComponentDescriptor {
    constructor() {
        super();
        const KnownCategory = rs.mimic.KnownCategory;
        const BasicType = rs.mimic.BasicType;
        const PropertyDescriptor = rs.mimic.PropertyDescriptor;
        configureCalendarBaseDescriptor(this);

        this.add(new PropertyDescriptor({ name: "inCnlNum", displayName: "Input channel 1", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "outCnlNum", displayName: "Output channel 1", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "secondInCnlNum", displayName: "Input channel 2", category: KnownCategory.DATA, type: BasicType.INT }));
        this.add(new PropertyDescriptor({ name: "secondOutCnlNum", displayName: "Output channel 2", category: KnownCategory.DATA, type: BasicType.INT }));

        this.add(new PropertyDescriptor({ name: "labelFrom", displayName: "Label 1", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        this.add(new PropertyDescriptor({ name: "labelTo", displayName: "Label 2", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        this.add(new PropertyDescriptor({ name: "buttonText", displayName: "Button text", category: KnownCategory.APPEARANCE, type: BasicType.STRING }));
        addLabelStyleProperties(this);
        addInputStyleProperties(this);
        addButtonStyleProperties(this);
        addCommandFormatProperty(this);
        this.add(new PropertyDescriptor({ name: "value1", displayName: "Value 1", category: KnownCategory.MISC, isBrowsable: false, type: BasicType.STRING }));
        this.add(new PropertyDescriptor({ name: "value2", displayName: "Value 2", category: KnownCategory.MISC, isBrowsable: false, type: BasicType.STRING }));
    }
};

rs.mimic.CalendarRangeBottomDescriptor = class extends rs.mimic.CalendarDoubleRangeDescriptor {};
rs.mimic.CalendarRangeSideDescriptor = class extends rs.mimic.CalendarDoubleRangeDescriptor {};

function registerCalendarDescriptors() {
    let componentDescriptors = rs.mimic.DescriptorSet.componentDescriptors;
    componentDescriptors.set("CalendarAuto", new rs.mimic.CalendarAutoDescriptor());
    componentDescriptors.set("CalendarInput", new rs.mimic.CalendarInputDescriptor());
    componentDescriptors.set("CalendarButton", new rs.mimic.CalendarButtonDescriptor());
    componentDescriptors.set("CalendarRange", new rs.mimic.CalendarRangeDescriptor());
    componentDescriptors.set("CalendarRangeBottom", new rs.mimic.CalendarRangeBottomDescriptor());
    componentDescriptors.set("CalendarRangeSide", new rs.mimic.CalendarRangeSideDescriptor());
}

registerCalendarDescriptors();
