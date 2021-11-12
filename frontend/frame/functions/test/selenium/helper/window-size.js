const setWindowSize = async (driver, width, height) => {
  await driver.manage().window().setRect({
    width,
    height,
    x: 0,
    y: 0,
  });
};

const fullScreen = async (driver) => driver.manage().window().fullscreen();

const hugeScreen = async (driver) => setWindowSize(driver, 1200, 800);

const largeScreen = async (driver) => setWindowSize(driver, 900, 800);

const mediumScreen = async (driver) => setWindowSize(driver, 600, 800);

const smallScreen = async (driver) => setWindowSize(driver, 300, 800);

const sizes = () => [
  fullScreen,
  hugeScreen,
  largeScreen,
  mediumScreen,
  smallScreen,
];

module.exports = {
  fullScreen,
  hugeScreen,
  largeScreen,
  mediumScreen,
  smallScreen,
  sizes,
};
