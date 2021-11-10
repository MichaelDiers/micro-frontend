const loadLicenses = async () => {
  const licensesElement = document.getElementById('licenses'); // eslint-disable-line no-undef
  if (!licensesElement) {
    return;
  }

  const promises = address.licenses.map((url) => apiCall(url, 'GET')); // eslint-disable-line no-undef
  const values = await Promise.all(promises);
  const applications = {};
  values.forEach((jsonArray) => {
    jsonArray.forEach(({
      name,
      licenseType,
      link,
      installedVersion,
    }) => {
      let addApplication = false;
      if (!applications[name]) {
        applications[name] = [installedVersion];
        addApplication = true;
      } else if (!applications[name].includes(installedVersion)) {
        applications[name].push(installedVersion);
        addApplication = true;
      }

      if (addApplication) {
        const url = link.replace(/^(git\+?)/i, '').replace(/^(:)/i, 'https:');
        addNewElement({ // eslint-disable-line no-undef
          parent: licensesElement,
          elementName: 'a',
          innerText: `${name} ${installedVersion} (${licenseType})`,
          attributes: [{ name: 'href', value: url }],
        });
      }
    });
  });
};

if (document.getElementById('id_c29b58e4b3ab492692b4bd06dc90aeb5')) { // eslint-disable-line no-undef
  loadLicenses().catch(handleError); // eslint-disable-line no-undef
}
