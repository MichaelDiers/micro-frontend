const express = require('express');

/**
 * Initialize the account router.
 * @param {object} options An object containing the paramter to initialize the router.
 * @returns A new express.Router or the given router from the options.
 */
const initialize = (options = {}) => {
  const {
    controller,
    router = express.Router(),
  } = options;

  router.get('/', async (req, res) => {
    const result = await controller.index();
    if (result) {
      const { view, options: viewOptions } = result;
      res.render(view, viewOptions);
    }
  });

  router.get('/logon', async (req, res) => {
    const result = await controller.logon();
    if (result) {
      const { view, options: viewOptions } = result;
      res.render(view, viewOptions);
    }
  });

  return router;
};

module.exports = initialize;