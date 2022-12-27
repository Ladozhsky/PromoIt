import express from 'express';
import controller from '../controllers/campaign.controller';

const router = express.Router();

router.get('/campaigns', controller.getCampaigns);
router.get('/campaigns-by-userId/:id', controller.getCampaignsByUserId);
router.post('/campaigns', controller.addCampaign);



export default { router };