const { Given, When, Then } = require("@badeball/cypress-cucumber-preprocessor");

When("navega al módulo de Médicos {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
});

Then("la página de Médicos debe cargarse correctamente", () => {
  cy.url().should("include", "/Medicos");
});

Given("que un usuario con rol Usuario inicia sesión con correo {string} y clave {string}", (correo, clave) => {
  cy.visit("/Acceso/Login");
  cy.get("#loginCorreo").clear().type(correo);
  cy.get("#loginClave").clear().type(clave);
  cy.get("form").filter(':contains("Iniciar Sesión"), :has(#loginCorreo)').submit();
  cy.url().should("include", "/Home/Index");
});

When("intenta navegar al módulo de Médicos {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
});

Then("debe ser redirigido a la página de acceso denegado {string}", (pathEsperado) => {
  cy.url().should("include", pathEsperado);
});
