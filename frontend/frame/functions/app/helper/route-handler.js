const initialize = (options = {}) => {
  const {
    config: {
      logger,
      url: {
        error500,
      },
    },
  } = options;

  const handle = (res, promise) => {
    promise.then(({ view, options: viewOptions }) => {
      res.render(view, viewOptions);
    }).catch((error) => {
      logger(error);
      res.redirect(303, error500);
    });
  };

  return handle;
};

module.exports = initialize;
