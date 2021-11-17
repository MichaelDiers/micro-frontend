const express = require('express');
const language = require('../language/language');

/**
 * Initialize options used for pug processing. The options are set to res.locals.pugOptions.
 * @param {object} options Options contain an optional router and the application configuration.
 * @returns The given router from the options or a new express.Router.
 */
const initialize = (options = {}) => {
  const {
    config: {
      route: {
        frame: frameRoute,
        license: licenseRoute,
        pictureCredit: pictureCreditRoute,
        version: versionRoute,
      },
      url: {
        baseUrl,
        cssFiles,
        jsFiles,
        publicUrl,
      },
      version,
    },
    router = express.Router(),
  } = options;

  router.use((req, res, next) => {
    const pugOptions = {
      cssFiles,
      jsFiles,
      favicon: [
        {
          rel: 'apple-touch-icon',
          sizes: '180x180',
          href: `${publicUrl}/apple-touch-icon.png`,
          type: 'image/png',
        },
        {
          rel: 'icon',
          sizes: '32x32',
          href: `${publicUrl}/favicon-32x32.png`,
          type: 'image/png',
        },
        {
          rel: 'icon',
          sizes: '16x16',
          href: `${publicUrl}/favicon-16x16.png`,
          type: 'image/png',
        },
      ],
      footer: {
        licenseCreditsUrl: `${baseUrl}/${res.locals.lang}${licenseRoute}`,
        pictureCreditUrl: `${baseUrl}/${res.locals.lang}${pictureCreditRoute}`,
        versionUrl: `${baseUrl}/${res.locals.lang}${versionRoute}`,
        languages: language.supported.filter((lang) => lang !== res.locals.lang).map((lang) => {
          const result = { text: language.translate(lang, lang), url: `${baseUrl}/${lang}${req.originalUrl.substring(3)}` };
          return result;
        }),
      },
      header: {
        homeUrl: `${baseUrl}/${res.locals.lang}${frameRoute}`,
      },
      version,
    };

    res.locals.pugOptions = pugOptions;
    next();
  });

  return router;
};

module.exports = initialize;
