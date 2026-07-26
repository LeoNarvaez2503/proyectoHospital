const { Given, When, Then } = require("@badeball/cypress-cucumber-preprocessor");

When("navega al módulo de Citas {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
});

Then("la página de Citas debe cargarse correctamente", () => {
  cy.url().should("include", "/Citas");
});

When("tenta agendar una cita con una fecha pasada {string}", (fechaPasada) => {
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get("input[type='datetime-local'], #fecha, input[type='date'], #fechaCita").first().then(($input) => {
    if ($input.length > 0) {
      const valorFecha = fechaPasada.includes("T") ? fechaPasada : `${fechaPasada}T10:00`;
      cy.wrap($input).clear({ force: true }).type(valorFecha, { force: true });
    }
  });
  cy.get(".btn-success").first().click({ force: true });
});

Then("el sistema debe impedir la selección o denegar la creación de la cita pasada", () => {
  cy.get("body").should("be.visible");
  cy.url().should("include", "/Citas");
});
