<?xml version="1.0" encoding="utf-8"?>
<Faceplate editorVersion="6.0.0.0">
  <Dependencies />
  <Document>
    <Size>
      <Width>444</Width>
      <Height>44</Height>
    </Size>
  </Document>
  <Components>
    <Text>
      <ID>1</ID>
      <Enabled>True</Enabled>
      <Location>
        <X>6</X>
        <Y>6</Y>
      </Location>
      <Name>label</Name>
      <Size>
        <Width>108</Width>
        <Height>32</Height>
      </Size>
      <Text>Date and time</Text>
      <Visible>True</Visible>
    </Text>
    <CalendarInput>
      <ID>2</ID>
      <AutoSend>False</AutoSend>
      <CommandFormat>Double</CommandFormat>
      <Enabled>True</Enabled>
      <InCnlNum>0</InCnlNum>
      <Location>
        <X>122</X>
        <Y>6</Y>
      </Location>
      <Name>input</Name>
      <OutCnlNum>0</OutCnlNum>
      <Size>
        <Width>198</Width>
        <Height>32</Height>
      </Size>
      <Visible>True</Visible>
    </CalendarInput>
    <Button>
      <ID>3</ID>
      <ClickAction>
        <ActionType>ExecuteScript</ActionType>
        <ChartArgs />
        <CommandArgs>
          <ShowDialog>True</ShowDialog>
          <CmdVal>0</CmdVal>
        </CommandArgs>
        <LinkArgs>
          <Url />
          <UrlParams>
            <Enabled>False</Enabled>
            <Param0 />
            <Param1 />
            <Param2 />
            <Param3 />
            <Param4 />
            <Param5 />
            <Param6 />
            <Param7 />
            <Param8 />
            <Param9 />
          </UrlParams>
          <ViewID>0</ViewID>
          <Target>Self</Target>
          <ModalWidth>Normal</ModalWidth>
          <ModalHeight>0</ModalHeight>
        </LinkArgs>
        <Script>// Get the input value from the CalendarInput inside this faceplate
var inputElem = this.closest('.faceplate').find('input[type="datetime-local"]');
if (inputElem.length) {
    var dateVal = inputElem.val();
    if (dateVal) {
        var dt = new Date(dateVal);
        var oaDate = dt.getTime() / 86400000 + 25569;
        mainApi.sendCommand(1, oaDate, false, null, function(dto) {
            if (!dto.ok) console.error('Command failed', dto.msg);
        });
    }
}</Script>
      </ClickAction>
      <Enabled>True</Enabled>
      <Location>
        <X>328</X>
        <Y>6</Y>
      </Location>
      <Name>button</Name>
      <Size>
        <Width>110</Width>
        <Height>32</Height>
      </Size>
      <Text>Set</Text>
      <Visible>True</Visible>
    </Button>
  </Components>
  <Images />
</Faceplate>
