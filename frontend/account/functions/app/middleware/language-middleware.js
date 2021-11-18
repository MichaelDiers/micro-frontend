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

  language.supported.forEach((lang) => {
    router.use(`/${lang}/*`, (req, res, next) => {
      res.locals.lang = lang;
      res.locals.translations = language.translations(lang);
      next();
    });
  });

  return router;
};

module.exports = initialize;
