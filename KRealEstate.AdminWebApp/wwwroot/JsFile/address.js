var AddressController = function () {
    this.initialize = function () {
        GetDistrictByProvinceId();
        GetWardByDistrctId();
    }
    function GetDistrictByProvinceId() {
        $(document).ready(function () {
            $("#ProviceCode").change(function () {
                var provinceId = $(this).val();
                //debugger
                $.ajax({
                    type: "post",
                    url: "/Register/GetDistrictByProvince?provinceId=" + provinceId,
                    contentType: "html",
                    success: function (response) {
                        //debugger
                        $("#DistrictCode").empty();
                        $("#DistrictCode").append(response);
                    }
                })
            })
        })
    }
    function GetWardByDistrctId() {
        $(document).ready(function () {
            $('#DistrictCode').change(function () {
                var districtCode = $(this).val();
                $.ajax({
                    type: 'post',
                    url: '/Register/GetWardByDistrict?districtId=' + districtCode,
                    contentType: 'html',
                    success: function (response) {
                        $("#WardCode").empty();
                        $("#WardCode").append(response);
                    }
                })
            })
        });
    }
}