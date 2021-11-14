const compression = require('compression');
const express = require('express');
const helmet = require('helmet');

/**
 * Initialize basic middleware used for all routes, like helmet and compression.
 * @param {object} options Options including an optional router.
 * @returns The given router from options or a new express.Router.
 */
const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  router.use(helmet());
  router.use(compression());
  router.use(express.urlencoded({ extended: false }));
  router.use(express.json());

  return router;
};

module.exports = initialize;