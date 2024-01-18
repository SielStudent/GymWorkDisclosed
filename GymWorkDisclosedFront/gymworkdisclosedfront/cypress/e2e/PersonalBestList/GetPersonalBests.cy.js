import { auth } from "../../../app/components/firebase/firebase.config"
const base_url = "http://localhost:3000/"

describe("Personal Bests Page", () => {
  it("should display the personal bests of the user", () => {
    cy.visit(base_url)
    cy.wait(5000)
    cy.signInWithEmailAndPassword(auth, 'test@test.com', 'testpassword')
    
    cy.wait(1000)

    cy.get("a").contains("Personal Bests").click()
    cy.wait(2000)
  
    cy.get('h1').contains('Arm Curls')
      .parent()
      .find('h3').contains('Best Time')
      .invoke('text')
      .should('equal', 'Best Time: 300 seconds')
  })
})