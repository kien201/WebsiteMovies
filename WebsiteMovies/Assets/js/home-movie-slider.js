function InitSlider(slider, delayTime) {
    // resposive slider
    const vw = document.documentElement.clientWidth;
    let sliderColumn = 4;
    if (vw <= 768) sliderColumn = 3;
    if (vw <= 450) sliderColumn = 2;
    const sliderItemList = slider.querySelectorAll('.slider-item');
    sliderItemList.forEach(item => {
        item.style.width = `calc(${slider.clientWidth / sliderColumn + 2}px - 0.5rem)`;
    });
    // move slider item
    let sliderIndex = 0;
    function BtnSlidePress(action) {
        const sliderOuter = slider.querySelector('.slider-outer');
        const sliderItemList = sliderOuter.querySelectorAll('.slider-item');
        const itemWidth = sliderItemList[0].offsetWidth;
        const itemMargin = parseInt(window.getComputedStyle(sliderItemList[0]).getPropertyValue("margin-right"));
        if (action == 'next') sliderIndex++;
        else if (action == 'prev') sliderIndex--;
        if (sliderIndex >= sliderItemList.length - (sliderColumn - 1)) sliderIndex = 0;
        if (sliderIndex < 0) sliderIndex = sliderItemList.length - sliderColumn;
        sliderOuter.setAttribute("style", `transform: translateX(-${sliderIndex * (itemWidth + itemMargin)}px);`);
    }
    slider.querySelector('.btn-left').addEventListener("click", () => BtnSlidePress("prev"))
    slider.querySelector('.btn-right').addEventListener("click", () => BtnSlidePress("next"))
    if (delayTime > 0) {
        let sliderInterval = setInterval(() => BtnSlidePress("next"), delayTime)
        slider.addEventListener("mouseover", function () {
            clearInterval(sliderInterval);
        })
        slider.addEventListener("mouseout", function () {
            sliderInterval = setInterval(() => BtnSlidePress("next"), delayTime)
        })
    }
}