import { auth } from "../../../app/components/firebase/firebase.config"
const base_url = "http://localhost:3000/"

describe("Workouts", () => {
    it("should display the workouts of the user", () => {
        cy.visit(base_url)
        cy.wait(5000)
        cy.signInWithEmailAndPassword(auth, 'test@test.com', 'testpassword')
        
        cy.wait(1000)

        cy.get("a").contains("Workouts").click()
        cy.get('.workoutDetails').invoke('attr', 'open', ' ')
        cy.get(".setDetails").invoke('attr', 'open', ' ')
        cy.get('h1').contains('Arm Curls')
        .parent()
        .find('h4').contains('Reps')
        .invoke('text')
        .should('equal', 'Reps: 5')
    })
})