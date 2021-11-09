const express = require('express');

const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  router.get('/', async (req, res) => res.send('hello world!'));

  return router;
};

module.exports = initialize;
