/**
 * Initialize the frame controller that handles the index page of the application.
 * @returns A controller object that handles the index page.
 */
const initialize = () => {
  const controller = {

    /**
     * Render the index page of the frame route.
     * @returns An object that contains the name of the view and optional render
     * parameters as { view, options }.
     */
    index: async () => {
      const result = {
        view: 'frame/index',
      };

      return result;
    },
  };

  return controller;
};

module.exports = initialize;
