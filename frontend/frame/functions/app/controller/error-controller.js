const initialize = () => {
  const controller = {
    error404: async () => {
      const result = {
        view: 'error/404',
      };

      return result;
    },
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
