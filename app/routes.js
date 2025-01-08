//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()

// Add your routes here
// Run this code when a form is submitted to 'adequate-onstreet-parking-answer'
router.post('/adequate-onstreet-parking-answer', function (req, res) {

    // Make a variable and give it the value from 'adequate-onstreet-parking'
    var adequate = req.session.data['adequate-onstreet-parking']
  
    // Check whether the variable matches a condition
    if (adequate == "Yes"){
      // Send user to next page
      res.redirect('/enter-VRN')
    } else {
      // Send user to ineligible page
      res.redirect('/ineligible')
    }
})