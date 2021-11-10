const express = require('express');

const initialize = (options = {}) => {
  const {
    config: {
      url: {
        publicUrl,
      },
    },
    router = express.Router(),
  } = options;

  router.use((req, res, next) => {
    const pugOptions = {
      lang: 'de',
      css_files: [],
      js_files: [],
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