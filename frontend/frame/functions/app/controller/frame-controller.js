const initialize = () => {
  const controller = {
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
