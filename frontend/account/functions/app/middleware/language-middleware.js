const express = require('express');
const language = require('../language/language');

/**
 * Initialize translations for specific languages. The options are set to res.locals.pugOptions.
 * @param {object} options Options contain an optional router.
 * @returns The given router from the options or a new express.Router.
 */
const initialize = (options = {}) => {
  const { router = express.Router() } = options;

  router.use((req, res, next) => {
    res.locals.lang = language.default;
    res.locals.translations = language.translations(language.default);
    next();
  });

  router.use('/de/*', (req, res, next) => {
    res.locals.lang = language.de;
    res.locals.translations = language.translations(res.locals.lang);
    next();
  });

  router.use('/en/*', (req, res, next) => {
    res.locals.lang = language.en;
    res.locals.translations = language.translations(res.locals.lang);
    next();
  });

  return router;
};

module.exports = initialize;
