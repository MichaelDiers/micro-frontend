const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    baseurl: baseUrl,
    corsfeframe: corsFeFrame,
  } = firebaseFunctions.config().account;

  const config = {
    logger: firebaseFunctions.logger.error,
    route: {
      account: '/account',
    },
    statics: {
      folder: 'app/public',
      route: '/public',
    },
    url: {
      corsFeFrame,
    },
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  return config;
};

module.exports = initialize;
