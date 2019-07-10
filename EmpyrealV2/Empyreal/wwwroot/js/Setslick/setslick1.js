// PRODUCTS SLICK
$('#product-slick-1').slick({
    slidesToShow: 6,
    slidesToScroll: 4,
    autoplay: true,
    infinite: true,
    speed: 300,
    dots: true,
    arrows: false,
    appendDots: '.product-slick-dots-1',
    responsive: [{
        breakpoint: 991,
        settings: {
            slidesToShow: 1,
            slidesToScroll: 1
        }
    },
    {
        breakpoint: 480,
        settings: {
            dots: false,
            arrows: true,
            slidesToShow: 1,
            slidesToScroll: 1
        }
    }
    ]
});