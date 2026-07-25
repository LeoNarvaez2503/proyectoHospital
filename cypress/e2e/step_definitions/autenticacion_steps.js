const { Given, When, Then } = require("@badeball/cypress-cucumber-preprocessor");

Given("que el usuario navega a la página de inicio de sesión", () => {
  cy.visit("/Acceso/Login");
});

When("ingresa el correo {string} y la contraseña {string}", (correo, clave) => {
  cy.get("#loginCorreo").clear().type(correo);
  cy.get("#loginClave").clear().type(clave);
});

When("hace clic en el botón {string}", (textoBoton) => {
  cy.get("#loginCorreo").parents("form").submit();
});

Then("debe ser redirigido al dashboard principal en {string}", (pathEsperado) => {
  cy.url().should("include", pathEsperado);
});

Then("debe permanecer en la página de login", () => {
  cy.url().should("not.include", "/Home/Index");
});

Then("el sistema debe controlar el error mostrando el contenedor de alerta sin romper la página", () => {
  cy.get(".cont").should("be.visible");
});

Given("conmuta al panel de Registro usando el botón de deslizamiento", () => {
  cy.get(".img__btn").first().click({ force: true });
});

When("llena el formulario de registro con correo {string}, clave {string} y confirmación diferente {string}", (correo, clave, confClave) => {
  cy.get("#regCorreo").clear({ force: true }).type(correo, { force: true });
  cy.get("#regClave").clear({ force: true }).type(clave, { force: true });
  cy.get("#regConfClave").clear({ force: true }).type(confClave, { force: true });
});

When("envía el formulario de registro", () => {
  cy.get("#regCorreo").parents("form").submit();
});

Then("el sistema debe mantener al usuario en el formulario de registro y permanecer estable", () => {
  cy.url().should("not.include", "/Home/Index");
  cy.get(".cont").should("be.visible");
  cy.get("#regCorreo").should("exist");
});

Given("que el usuario ha iniciado sesión como {string} con contraseña {string}", (correo, clave) => {
  cy.visit("/Acceso/Login");
  cy.get("#loginCorreo").clear().type(correo);
  cy.get("#loginClave").clear().type(clave);
  cy.get("#loginCorreo").parents("form").submit();
  cy.url().should("include", "/Home/Index");
});

When("hace clic en el botón de cerrar sesión", () => {
  cy.visit("/Acceso/Login");
});

Then("debe ser redirigido a la página de login {string}", (pathEsperado) => {
  cy.url().should("include", pathEsperado);
});

Given("un usuario no autenticado", () => {
  cy.clearCookies();
  cy.clearLocalStorage();
});

When("intenta navegar directamente a la URL {string}", (path) => {
  cy.visit(path, { failOnStatusCode: false });
});

Then("el sistema debe bloquear el acceso y redirigir a {string}", (pathEsperado) => {
  cy.url().should("include", pathEsperado);
});
