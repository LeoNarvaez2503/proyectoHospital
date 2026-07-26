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

When("realiza una búsqueda de médicos con el parámetro {string}", (parametro) => {
  cy.get("input[type='search'], #txtBuscar, input.form-control").first().then(($input) => {
    if ($input.length > 0) {
      cy.wrap($input).clear({ force: true }).type(parametro, { force: true });
    }
  });
});

Then("la tabla de médicos no debe ejecutar la carga útil inyectada y permanecer estable", () => {
  cy.on("window:alert", (str) => {
    expect(str).to.not.equal("medico_xss");
  });
  cy.get("body").should("be.visible");
});
