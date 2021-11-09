const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    baseurl: baseUrl,
  } = firebaseFunctions.config().frame;

  const config = {
    logger: firebaseFunctions.logger.error,
    route: {
      error: '/error',
      frame: '/frame',
    },
    url: {
    },
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.error404 = `${baseUrl}${config.route.error}/404`;
  config.url.error500 = `${baseUrl}${config.route.error}/500`;

  return config;
};

module.exports = initialize;
