using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        [TestMethod()]
        public void DevisesControllerTest()
        {

        }

        [TestMethod()]
        public void GetAll_ReturnsListOfDevises()
        {
            // Arrange
            var controller = new DevisesController();

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Le retour n'est pas une liste de devises.");
            var devises = result.ToList();
            Assert.AreEqual(3, devises.Count, "La liste ne contient pas 3 devises.");
            CollectionAssert.Contains(devises, new Devise(1, "Dollar", 1.08), "La devise Dollar n'est pas dans la liste.");
            CollectionAssert.Contains(devises, new Devise(2, "Franc Suisse", 1.07), "La devise Franc Suisse n'est pas dans la liste.");
            CollectionAssert.Contains(devises, new Devise(3, "Yen", 120), "La devise Yen n'est pas dans la liste.");
        }

        [TestMethod()]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult"); // Test du type de retour
            Assert.IsNull(result.Result, "Erreur est pas null"); //Test de l'erreur
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise"); // Test du type du contenu (valeur) du retour
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques"); //Test de la devise récupérée
        }

        [TestMethod()]
        public void GetById_UnknownIdPassed_ReturnsNotFound()
        {
            // Arrange
            var controller = new DevisesController();

            // Act
            var result = controller.GetById(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Le retour n'est pas un ActionResult<Devise>.");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le retour n'est pas un NotFoundResult.");
            Assert.IsNull(result.Value, "La valeur retournée n'est pas null.");
        }

        [TestMethod()]
        public void Post_ValidDevisePassed_ReturnsCreatedResponse()
        {
            // Arrange
            var controller = new DevisesController();
            var newDevise = new Devise(4, "Euro", 1.0);

            // Act
            var result = controller.Post(newDevise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Le retour n'est pas un ActionResult<Devise>.");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Le retour n'est pas un CreatedAtRouteResult.");
            var routeResult = result.Result as CreatedAtRouteResult;
            Assert.AreEqual(StatusCodes.Status201Created, routeResult.StatusCode, "Le statut retourné n'est pas 201 Created.");
            Assert.AreEqual(newDevise, routeResult.Value, "La devise retournée n'est pas correcte.");
        }

        [TestMethod()]
        public void Post_InvalidDevisePassed_ReturnsBadRequest()
        {
            // Arrange
            var controller = new DevisesController();
            var invalidDevise = new Devise(4, null, 1.0); // Devise invalide car le nom est null

            // Simuler une erreur de validation dans le ModelState
            controller.ModelState.AddModelError("NomDevise", "Le nom de la devise est requis.");

            // Act
            var result = controller.Post(invalidDevise);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult), "Le retour n'est pas un BadRequestObjectResult.");
        }

        [TestMethod()]
        public void Put_ValidDevisePassed_ReturnsNoContent()
        {
            // Arrange
            var controller = new DevisesController();
            var updatedDevise = new Devise(1, "Dollar", 1.09);

            // Act
            var result = controller.Put(1, updatedDevise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le retour n'est pas un NoContentResult.");
        }

        [TestMethod()]
        public void Put_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var controller = new DevisesController();
            var updatedDevise = new Devise(2, "Dollar", 1.09);

            // Act
            var result = controller.Put(1, updatedDevise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult), "Le retour n'est pas un BadRequestResult.");
        }

        [TestMethod()]
        public void Put_UnknownIdPassed_ReturnsNotFound()
        {
            // Arrange
            var controller = new DevisesController();
            var updatedDevise = new Devise(999, "Dollar", 1.09);

            // Act
            var result = controller.Put(999, updatedDevise);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult), "Le retour n'est pas un NotFoundResult.");
        }



        [TestMethod()]
        public void Delete_ExistingIdPassed_ReturnsOk()
        {
            // Arrange
            var controller = new DevisesController();

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Le retour n'est pas un ActionResult<Devise>.");
            Assert.IsNull(result.Result, "Erreur, le résultat n'est pas null.");
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Le retour n'est pas une Devise.");
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), result.Value, "La devise retournée n'est pas correcte.");
        }

        [TestMethod()]
        public void Delete_UnknownIdPassed_ReturnsNotFound()
        {
            // Arrange
            var controller = new DevisesController();

            // Act
            var result = controller.Delete(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Le retour n'est pas un ActionResult<Devise>.");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le retour n'est pas un NotFoundResult.");
            Assert.IsNull(result.Value, "La valeur retournée n'est pas null.");
        }


    }
}