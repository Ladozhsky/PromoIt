import express from 'express';
import controller from '../controllers/campaign.controller';

const router = express.Router();

router.get('/campaigns', controller.getCampaigns);


export default { router };