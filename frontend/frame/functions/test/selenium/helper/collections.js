const FrameIndexPage = require('../pages/frame-index-page');
const LicenseIndexPage = require('../pages/license-index-page');
const PictureCreditsIndexPage = require('../pages/picture-credits-index-page');

const pages = () => [
  new FrameIndexPage(),
  new LicenseIndexPage(),
  new PictureCreditsIndexPage(),
];

module.exports = {
  pages,
};
