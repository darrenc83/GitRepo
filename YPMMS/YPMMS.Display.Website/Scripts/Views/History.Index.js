var HistoryIndex = new function (){


    var thiscode = 0;


/**
 * Get JSON data from the server and initialise the views
 */
this.initialise = function (tebscode) { 

    thiscode = tebscode;
    initialiseSignalR();

};


function initialiseSignalR() {
    // Initialise the SignalR hub
    var machineHub = $.connection.machineHub;


    /**
     * Bag is altered is added
     */
    machineHub.client.updateBag = function (bag) {

        if (bag.code.Code != thiscode)
            return;

        var html = '<table class="mobile-responsive individual-system-information tebs-bag-info">';
        html += '<tr><th>Denomination</th><th>Qty</th><th>Total</th></tr>';
        var TotalCount = 0;
        for (var i = 0; i < bag.count.length; i++)
        {
            html += '<tr>';
            html += '<td>' + Format.asCurrency(bag.count[i].Value, bag.count[i].Country, true) + '</td>';
            html += '<td>' + bag.count[i].Number + '</td>';
            html += '<td>' + Format.asCurrency(bag.count[i].Tot, bag.count[i].Country, true) + '</td>';
            html += '</tr>';
            TotalCount += bag.count[i].Number;
        }
        for (var i = 0; i < bag.total.length; i++)
        {
            html += '<tr>';
            html += '<td>Grand Total</td>';
            html += '<td>' + TotalCount + '</td>';
            html += '<td>' + Format.asCurrency(bag.total[i].Value, bag.total[i].Country, true) + '</td>';
            html += '</tr>';
        }
        html += '</table>';

        $('#tebs-bag-summary').html(html);

       
        $("#tebs-bag-history tbody").empty();
        for(var i = 0; i < bag.history.length; i++)
        {
            var newRowContent = '<tr>';
            newRowContent += '<td>' + Format.asDateAndTime(bag.history[i].Timestamp) + '</td>';
            newRowContent += '<td>' + bag.history[i].MachineId + '</td>';
            newRowContent += '<td>' + bag.history[i].RecordType + '</td>';
            newRowContent += '<td>' + (bag.history[i].RejectCode == 'None' ? "" : bag.history[i].RejectCode) + '</td>';
            newRowContent += '<td>' + (bag.history[i].KeyId == 0 ? "" : bag.history[i].KeyId) + '</td>';
            newRowContent += '<td>' + (bag.history[i].Country == '...' ? "" : bag.history[i].Country) + '</td>';
            newRowContent += '<td>' + (bag.history[i].Value == 0 ? "" : Format.asCurrency(bag.history[i].Value,bag.history[i].Country))  + '</td>';
            newRowContent += '</tr>';
            $("#tebs-bag-history tbody").append(newRowContent);

        }

    };


    $.connection.hub.start().done();
};

}();