const appConfig = require('../../../.runtimeconfig.json');

const config = {
  url: {
    baseUrl: appConfig.frame.baseurl,
  },
  timeout: {
    ajax: 10000,
    checkPageId: 10000,
  },
};

config.url.frameIndex = `${config.url.baseUrl}/frame`;
config.url.licenseIndex = `${config.url.baseUrl}/license`;

module.exports = config;
