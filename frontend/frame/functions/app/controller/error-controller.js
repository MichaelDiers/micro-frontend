/**
 * Initialize the error controller that handles http status 404 and 500.
 * @returns An error controller object.
 */
const initialize = () => {
  const controller = {

    /**
     * Handle 404 error and render the 404 page.
     * @returns An object that contains the name of the view and optional render
     * parameters as { view, options }.
     */
    error404: async () => {
      const result = {
        view: 'error/404',
      };

      return result;
    },

    /**
     * Handle 500 error and render the 500 page.
     * @returns An object that contains the name of the view and optional render
     * parameters as { view, options }.
     */
    error500: async () => {
      const result = {
        view: 'error/500',
      };

      return result;
    },
  };

  return controller;
};

module.exports = initialize;
