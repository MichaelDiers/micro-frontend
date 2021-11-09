const compression = require('compression');
const express = require('express');
const helmet = require('helmet');

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
