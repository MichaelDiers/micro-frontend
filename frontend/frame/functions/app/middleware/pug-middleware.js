const express = require('express');

const initialize = (options = {}) => {
  const {
    config: {
      url: {
        cssFiles,
        jsFiles,
        licenses,
        licenseUrl,
        pictureCreditUrl,
        publicUrl,
        home: homeUrl,
      },
    },
    router = express.Router(),
  } = options;

  router.use((req, res, next) => {
    const pugOptions = {
      lang: 'de',
      cssFiles,
      jsFiles,
      licenseUrls: licenses,
      licenseUrl,
      pictureCreditUrl,
      homeUrl,
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
    };

    res.locals.pugOptions = pugOptions;
    next();
  });

  return router;
};

module.exports = initialize;
