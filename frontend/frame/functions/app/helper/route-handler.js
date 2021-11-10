/**
 * Initializes an route handler that handles the rendering of a page and catches
 * unexpected errors.
 * @param {object} options An object that contains the configuration of the application.
 * @returns A function that is called with an express.Response and a promise whose result
 * is an object containing a view name and view rendering options as { view, options }.
 */
const initialize = (options = {}) => {
  const {
    config: {
      logger,
      url: {
        error500,
      },
    },
  } = options;

  /**
   * Renders a page by using the result of the promise whose result is an
   * object { view, options } and handles unexpected error by redicting
   * to the error page.
   * @param {express.Response} res An express.Response that used for randering or redirecting.
   * @param {Promise} promise A Promise whose result is a { view, options } object.
   * @param {boolean} redirectOnError Indicates a redirect to 500 error page if true,
   *  sends status 500 otherwise. Should false for error pages (infitite loop).
   */
  const handle = (res, promise, redirectOnError = true) => {
    promise.then(({ view, options: viewOptions }) => {
      res.render(view, viewOptions, (err, html) => {
        if (err) {
          logger(err);
          if (redirectOnError === true) {
            res.redirect(303, error500);
          } else {
            res.send(500);
          }
        } else {
          res.send(html);
        }
      });
    }).catch((error) => {
      logger(error);
      if (redirectOnError === true) {
        res.redirect(303, error500);
      } else {
        res.status(500);
      }
    });
  };

  return handle;
};

module.exports = initialize;
