import express from 'express';
import controller from '../controllers/company.controller';

const router = express.Router();

router.get('/companies', controller.getCompanies);
//router.post('/companies', controller.addCompany);



export default { router };