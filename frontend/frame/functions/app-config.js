const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    accountfehost: accountFeHost,
    accountfepath: accountFePath,
    baseurl: baseUrl,
    version,
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
      account: '/account',
      error: '/error',
      frame: '/frame',
      license: '/license',
      pictureCredit: '/pictureCredit',
      version: '/version',
    },
    statics: {
      folder: 'app/public',
      route: '/public',
    },
    url: {
      accountFeHost,
      baseUrl,
    },
    version,
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  config.url.jsFiles = config.jsFiles.map((jsFile) => `${config.url.publicUrl}/${jsFile}`);
  config.url.accountFePublic = `${accountFeHost}${accountFePath}/public`;
  config.url.jsFiles.push(`${config.url.accountFePublic}/client.min.js`);
  config.url.cssFiles = config.cssFiles.map((cssFile) => `${config.url.publicUrl}/${cssFile}`);
  return config;
};

module.exports = initialize;
