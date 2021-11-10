const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    baseurl: baseUrl,
    licenses,
  } = firebaseFunctions.config().frame;

  const config = {
    cssFiles: [
      'client.min.css',
    ],
    jsFiles: [
      'client.min.js',
    ],
    logger: firebaseFunctions.logger.error,
    route: {
      error: '/error',
      frame: '/frame',
      license: '/license',
    },
    statics: {
      folder: 'app/public',
      route: '/public',
    },
    url: {
      licenses: [],
    },
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.error404 = `${baseUrl}${config.route.error}/404`;
  config.url.error500 = `${baseUrl}${config.route.error}/500`;
  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  config.url.jsFiles = config.jsFiles.map((jsFile) => `${config.url.publicUrl}/${jsFile}`);
  config.url.cssFiles = config.cssFiles.map((cssFile) => `${config.url.publicUrl}/${cssFile}`);
  config.url.licenseUrl = `${baseUrl}${config.route.license}`;

  if (licenses) {
    config.url.licenses = licenses.split('##');
  }

  return config;
};

module.exports = initialize;
