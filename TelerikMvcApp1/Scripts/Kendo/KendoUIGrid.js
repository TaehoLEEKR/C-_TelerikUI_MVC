
//$(document).ready(function () {
//    KendoUIGridHelper.GenerateKendoGrid();

//});
//var KendoUIGridManager = {
//    gridDataSource: function () {
//        var gridDataSource = new kendo.data.DataSource({
//            dataType: "json",
//            serverPaging: true,
//            serverSorting: true,
//            serverFiltering: true,
//            allowUnsort: true,
//            pageSize: 10,
//            transport: {
//                read: {
//                    url: '..',
//                    dataType: "json",
//                    contentType: "application/json; charset-utf-8"
//                },
//                parameterMap: function (requst) {
//                    return Json.stringify(request);
//                }

//            },
//            shcema: { data: "Items", total: "TotalCount" }

//        });
//        return gridDataSource;
//    }
//};

//var KendoUIGridHelper = {

//    GenerateKendoGrid: function () {
//        $("#gridKendoUI").kendoGrid({
//            dataSource: KendoUIGridManager.gridDataSource(),
//            pageable: {
//                refresh: true,
//                serverPaging: true,
//                serverFiltering: true,
//                serverSorting : true
//            },
//                xheight: 450,
//                filterable: true,
//                sortable: true,
//                columns: KendoUIGridHelper.Columns(),
//                editable: false,
//                navigatble: true,
//                selectable:"row"
//        });
//    },
//    Columns: function () {
//        return columns = [
//            { field: "News_Num", title: "Id", width: 50 },
//            { field: "등록일자", title: "등록일자", width: 80 },
//            { field: "등록자", title: "등록자", width: 80 },
//            { field: "공연예약", title: "공연예약", width: 80 },
//            { field: "상세내역", title: "상세내역", width: 80 },
//            { field: "표시기간", title: "표시기간", width: 80 }
        
//        ];
//    }
  

//}