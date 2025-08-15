var app = angular.module("offerinputapp", ['ngSanitize']);
app.controller("offerinputController", ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.SetOfferInput = function () {

        let adi = document.getElementById("UrunAdi").value;
        let fiyat = document.getElementById("Fiyat").value;
        let adet = document.getElementById("Adet").value;

        $http({
            method: "POST",
            url: "/JS/SetOffer",
            dataType: 'json',
            data: JSON.stringify({ adi, fiyat, adet }),
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.offerlist = response.data;
            $('#saveAllModal').modal('hide');
            document.getElementById('lblSuccessPopup').textContent = "Kalem Eklendi...";
            $('#successModal').modal('show');
        }
        function Error(error) {
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.GetOffers = function () {

        $http({
            method: "POST",
            url: "/JS/GetOffers",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.offerlist = response.data;

        }
        function Error(error) {
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.SetID = function (id) {
        document.getElementById("ID").value = id;
    }

    $scope.RemoveOffer = function () {
        let sira = document.getElementById("ID").value;
        $http({
            method: "POST",
            url: "/JS/RemoveOffer",
            dataType: 'json',
            data: JSON.stringify(sira),
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.offerlist = response.data;
            $('#removeModal').modal('hide');

        }
        function Error(error) {
            $('#removeModal').modal('hide');
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.RemoveAllOffer = function () {
        $http({
            method: "POST",
            url: "/JS/RemoveAllOffer",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.offerlist = response.data;
            $('#removeAllModal').modal('hide');

        }
        function Error(error) {
            $('#removeAllModal').modal('hide');
            document.getElementById('lblErrorPopup').textContent = error.data;
            $('#errorModal').modal('show');
        }
    }

    $scope.SaveAllOffer = function () {
        $http({
            method: "POST",
            url: "/JS/SaveAllOffer",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(Success, Error);

        function Success(response) {

            $scope.offerlist = response.data;
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
