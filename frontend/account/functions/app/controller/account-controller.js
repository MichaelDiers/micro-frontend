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
    index: async () => {
      const result = {
        view: 'account/index',
      };

      return result;
    },

    /**
     * Render the logon page of the account route.
     * @returns An object that contains the name of the view and optional render
     *   parameters as { view, options }.
     */
    logon: async (lang) => {
      const result = {
        view: 'account/logon',
        options: {
          signupUrl: `__LOCAL__URL__/${lang}/account/signup`,
        },
      };

      return result;
    },

    logonPost: async (body = {}) => {
      const { email } = body;

      const result = {
        view: 'account/welcomeBack',
        options: {
          name: email,
        },
      };

      return result;
    },

    signupGet: async (lang) => {
      const result = {
        view: 'account/signup',
        options: {
          logonUrl: `__LOCAL__URL__/${lang}/account/logon`,
        },
      };

      return result;
    },

    signupPost: async (body = {}) => {
      const { email } = body;

      const result = {
        view: 'account/welcomeBack',
        options: {
          name: email,
        },
      };

      return result;
    },
  };

  return controller;
};

module.exports = initialize;
