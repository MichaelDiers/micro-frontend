const express = require('express');

const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  router.use((req, res, next) => {
    const pugOptions = {
      lang: 'de',
      css_files: [],
      js_files: [],
    };

    res.locals.pugOptions = pugOptions;
    next();
  });

  return router;
};

module.exports = initialize;
