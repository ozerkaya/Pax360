var app = angular.module("orderinputapp", ['ngSanitize']);
app.controller("orderinputController", ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.SetOrderInput = function () {

        let cihazmodeli = document.getElementById("CihazModeli").value;
        let miktar = document.getElementById("Adet").value;
        let birimfiyat = document.getElementById("Fiyat").value;

        $http({
            method: "POST",
            url: "/JS/SetOrder",
            dataType: 'json',
            data: JSON.stringify({ cihazmodeli, miktar, birimfiyat }),
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.orderlist = response.data;
            $('#saveAllModal').modal('hide');
            document.getElementById('lblSuccessPopup').textContent = "Kalem Eklendi...";
            $('#successModal').modal('show');
        }
        function Error(error) {
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.GetOrders = function () {

        $http({
            method: "POST",
            url: "/JS/GetOrders",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.orderlist = response.data;

        }
        function Error(error) {
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.SetID = function (id) {
        document.getElementById("ID").value = id;
    }

    $scope.RemoveOrder = function () {
        let sira = document.getElementById("ID").value;
        $http({
            method: "POST",
            url: "/JS/RemoveOrder",
            dataType: 'json',
            data: JSON.stringify(sira),
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.orderlist = response.data;
            $('#removeModal').modal('hide');

        }
        function Error(error) {
            $('#removeModal').modal('hide');
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.RemoveAllOrder = function () {
        $http({
            method: "POST",
            url: "/JS/RemoveAllOrder",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.orderlist = response.data;
            $('#removeAllModal').modal('hide');

        }
        function Error(error) {
            $('#removeAllModal').modal('hide');
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.SaveAllOrder = function () {
        $http({
            method: "POST",
            url: "/JS/SaveAllOrder",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.orderlist = response.data;
            $('#saveAllModal').modal('hide');
            document.getElementById('lblSuccessPopup').textContent = "İşlem başarılı...";
            $('#successModal').modal('show');

        }
        function Error(error) {
            $('#saveAllModal').modal('hide');
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }
}]);
