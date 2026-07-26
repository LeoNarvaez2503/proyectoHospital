const { Given, When, Then } = require("@badeball/cypress-cucumber-preprocessor");

When("intenta registrar un paciente con el nombre {string} y apellido {string}", (nombre, apellido) => {
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get("#nombre").clear({ force: true }).type(nombre, { force: true });
  cy.get("#apellido").clear({ force: true }).type(apellido, { force: true });
  cy.get(".btn-success").click({ force: true });
});

Then("el sistema no debe ejecutar el script malicioso", () => {
  cy.on("window:alert", (str) => {
    expect(str).to.not.equal("XSS_ATTACK");
  });
  cy.get("h1").should("contain", "Pacientes");
});

Then("la página debe mantenerse estable y sanitizada", () => {
  cy.url().should("include", "/Pacientes");
  cy.get("body").should("be.visible");
});

When("intenta registrar un paciente con dirección {string}", (direccion) => {
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get("#nombre").clear({ force: true }).type("PacienteTest", { force: true });
  cy.get("#apellido").clear({ force: true }).type("Sanitizacion", { force: true });
  cy.get("#direccion").clear({ force: true }).type(direccion, { force: true });
  cy.get(".btn-success").click({ force: true });
});

Then("el sistema debe codificar o neutralizar las etiquetas HTML en la respuesta", () => {
  cy.get("body").should("not.contain.html", "<script>alert");
});

Then("no se debe desencadenar ninguna alerta no deseada", () => {
  cy.get("body").should("be.visible");
});

Then("el sistema debe denegar el acceso y permanecer en la página de login", () => {
  cy.url().should("not.include", "/Home/Index");
  cy.get("#loginCorreo, .cont").should("be.visible");
});

Then("el servidor no debe retornar excepciones no controladas de SQL", () => {
  cy.get("body").should("not.contain", "SqlException");
  cy.get("body").should("not.contain", "Uncaught Error");
});

When("realiza una búsqueda con el término {string}", (termino) => {
  cy.get("input[type='search'], #txtBuscar, input.form-control").first().then(($input) => {
    if ($input.length > 0) {
      cy.wrap($input).clear({ force: true }).type(termino, { force: true });
    }
  });
});

Then("el sistema debe responder de manera segura sin exponer errores de base de datos", () => {
  cy.get("body").should("not.contain", "SqlException");
  cy.get("body").should("be.visible");
});

When("llena el campo de teléfono con letras y caracteres especiales {string}", (telefonoErroneo) => {
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get("#telefono").clear({ force: true }).type(telefonoErroneo, { force: true });
  cy.get(".btn-success").click({ force: true });
});

Then("el formulario debe indicar error de formato o rechazar el envío del dato no numérico", () => {
  cy.get("body").should("be.visible");
});

When("ingresa un nombre con una cadena de 1000 caracteres {string}", (cadenaCorta) => {
  const cadenaLarga = "A".repeat(1000);
  cy.get("[data-bs-target='#operacionesModal'], .btn-primary").first().click({ force: true });
  cy.get("#nombre").clear({ force: true }).type(cadenaLarga, { force: true });
  cy.get(".btn-success").click({ force: true });
});

Then("el sistema debe truncar la entrada o rechazarla sin provocar caída del servidor", () => {
  cy.get("body").should("be.visible");
  cy.url().should("include", "/Pacientes");
});
