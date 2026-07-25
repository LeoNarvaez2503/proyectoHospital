const { Given, When, Then } = require("@badeball/cypress-cucumber-preprocessor");

When("navega al módulo de Citas {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
});

Then("la página de Citas debe cargarse correctamente", () => {
  cy.url().should("include", "/Citas");
});
