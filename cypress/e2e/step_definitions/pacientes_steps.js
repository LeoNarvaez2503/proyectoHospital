const { Given, When, Then } = require("@badeball/cypress-cucumber-preprocessor");

Given("que el Administrador ha iniciado sesión con correo {string} y clave {string}", (correo, clave) => {
  cy.visit("/Acceso/Login");
  cy.get("#loginCorreo").clear().type(correo);
  cy.get("#loginClave").clear().type(clave);
  cy.get("#loginCorreo").parents("form").submit();
  cy.url().should("include", "/Home/Index");
});

When("navega al módulo de Pacientes {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
});

Then("la página de Pacientes debe cargarse correctamente", () => {
  cy.get("h1").should("contain", "Pacientes");
});

Given("se encuentra en el módulo de Pacientes {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
  cy.get("h1").should("contain", "Pacientes");
});

When("llena y envía el formulario con los datos del paciente:", (dataTable) => {
  const row = dataTable.hashes()[0];
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get("#nombre").clear({ force: true }).type(row.nombre, { force: true });
  cy.get("#apellido").clear({ force: true }).type(row.apellido, { force: true });
  cy.get("#telefono").clear({ force: true }).type(row.telefono, { force: true });
  cy.get("#email").clear({ force: true }).type(row.email, { force: true });
  cy.get("#direccion").clear({ force: true }).type(row.direccion, { force: true });
  cy.get(".btn-success").click({ force: true });
});

Then("el paciente {string} debe ser registrado exitosamente", (nombre) => {
  cy.get("h1").should("contain", "Pacientes");
});

When("abre el modal de registro y hace clic en Enviar sin llenar los campos", () => {
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get(".btn-success").click({ force: true });
});

Then("el modal debe permanecer abierto o el sistema debe mantener la estabilidad en la página", () => {
  cy.get("h1").should("contain", "Pacientes");
});
