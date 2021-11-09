const express = require('express');

const initialize = (options = {}) => {
  const {
    controller,
    router = express.Router(),
  } = options;

  router.get('/', async (req, res) => {
    const { view, viewOptions } = await controller.index();
    res.render(view, viewOptions);
  });

  return router;
};

module.exports = initialize;
