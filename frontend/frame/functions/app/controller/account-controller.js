const language = require('../language/language');

/**
 * Initialize the account controller.
 * @returns The controller as an object.
 */
const initialize = () => {
  const controller = {
    /**
     * Render the index page of the account route.
     * @returns An object that contains the name of the view and optional render
     *   parameters as { view, options }.
     */
    index: async (url) => {
      const regex = new RegExp(`^/(${language.supported.join('|')})/`);
      const result = {
        view: 'account/index',
        options: {
          // accountRequest: `${url.replace(/^\/(de|en|fr)\//, '').replace(/\//g, '')}Request`,
          accountRequest: `${url.replace(regex, '').replace(/\//g, '')}Request`,
        },
      };
      return result;
    },
  };

  return controller;
};

module.exports = initialize;
