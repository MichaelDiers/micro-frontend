/**
 * Initialize the picture-credit controller.
 * @returns The controller as an object.
 */
const initialize = () => {
  const controller = {
    /**
     * Render the index page of the picture-credit route.
     * @returns An object that contains the name of the view and optional render
     *   parameters as { view, options }.
     */
    index: async () => {
      const result = {
        view: 'picture-credit/index',
      };
      return result;
    },
  };

  return controller;
};

module.exports = initialize;
