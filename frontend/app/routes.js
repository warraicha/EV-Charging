//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()
const axios = require('axios');

router.post('/adequate-onstreet-parking-answer', function (req, res) {

    var adequate = req.session.data['adequate-onstreet-parking']
  
    if (adequate == "Yes"){
      res.redirect('/enter-VRN')
    } else {
      res.redirect('/ineligible')
    }
})

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
    const response = await axios.post('http://localhost:5016/api/applications', requestBody);

    console.log('Application submitted successfully:', response.data);

    res.redirect('/confirmation');
  } catch (error) {
    console.error('Error submitting application:', error.message);
  }
});