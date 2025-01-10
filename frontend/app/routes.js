//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()
const axios = require('axios');

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

// Route to handle form submission from "check-answers" page
router.post('/submit-application', async function (req, res) {
  const selectedAddress = req.session.data['address'];

  const addressParts = selectedAddress.split(','); 
  const line1 = addressParts[0]?.trim() || "Unknown"; 
  const city = addressParts[1]?.trim() || "Unknown";  
  const postcode = addressParts[2]?.trim() || "Unknown"; 
  
  const requestBody = {
    Name: req.session.data['application-name'],
    Email: req.session.data['application-email'],
    VehicleRegistrationNumber: req.session.data['enter-VRN'],
    Address: {
      Line1: line1,
      city: city,
      postcode: postcode
    }
  };

  try {
    // Make a POST request to the backend API
    const response = await axios.post('http://localhost:5016/api/applications', requestBody);

    console.log('Application submitted successfully:', response.data);

    res.redirect('/confirmation');
  } catch (error) {
    console.error('Error submitting application:', error.message);
  }
});