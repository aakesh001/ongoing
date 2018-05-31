
$(document).ready(function () {
    debugger
    if (window.location.pathname === '/Live/Index') {
        $('#live').addClass('active');
    }
    else if (window.location.pathname === '/Device/Index') {
        $('#device').addClass('active');
    }
    else if (window.location.pathname === '/AddDevice/Index') {
        $('#adddevice').addClass('active');
    }
    else if (window.location.pathname === '/CompanyDevice/Index') {
        $('#companydevice').addClass('active');
    }
    else if (window.location.pathname === '/Reports/Index') {
        $('#reports').addClass('active');
    } 
});
