$(function () {
    $(".choose-skin li").on("click", function () {
        var t = $("#layout"),
            e = $(this),
            i = $(".choose-skin li.active").data("theme");
        $(".choose-skin li").removeClass("active"), t.removeClass("theme-" + i), e.addClass("active"), t.addClass("theme-" + e.data("theme"));
    }),
        setTimeout(function () {
            $(".page-loader-wrapper").fadeOut();
        }, 50),
        $(".sidebar").metisMenu(),
        $('.navbar-form.search-form input[type="text"]')
            .on("focus", function () {
                $(this).animate({ width: "+=50px" }, 300);
            })
            .on("focusout", function () {
                $(this).animate({ width: "-=50px" }, 300);
            }),
        $(".btn-toggle-fullwidth").on("click", function () {
            $("body").hasClass("layout-fullwidth") ? $("body").removeClass("layout-fullwidth") : $("body").addClass("layout-fullwidth"), $(this).find(".fa").toggleClass("fa-arrow-left fa-arrow-right");
        }),
        $(".btn-toggle-offcanvas").on("click", function () {
            $("body").toggleClass("offcanvas-active");
        }),
        $("#main-content").on("click", function () {
            $("body").removeClass("offcanvas-active");
        }),
        $(".theme-rtl input:checkbox").on("click", function () {
            $(this).is(":checked") ? $("body").addClass("rtl_mode") : $("body").removeClass("rtl_mode");
        }),
        $(".minisidebar-active input:checkbox").on("click", function () {
            $(this).is(":checked") ? $("body").addClass("sidebar-mini") : $("body").removeClass("sidebar-mini");
        }),
        $(".sidebar-mini #left-sidebar").hover(
            function () {
                $(this).removeClass("mini");
            },
            function () {
                $(this).addClass("mini"),
                    $(".sidebar-mini .sidebar .tab-pane").removeClass("active show"),
                    $(".sidebar-mini .sidebar .nav-link").removeClass("active"),
                    $(".sidebar-mini .sidebar #hr_menu_nav_link").addClass("active"),
                    $(".sidebar-mini .sidebar #hr_menu").addClass("active show");
            })
       } );
    [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]')).map(function (t) {
        return new bootstrap.Tooltip(t);
    });

                 
                          
                           