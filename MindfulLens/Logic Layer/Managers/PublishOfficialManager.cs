using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Logic_Layer.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class PublishOfficialManager
    {
        private IPublishOfficialRepository publishOfficialRepository;
        private ICognitivePartRepository cognitivePartRepository;
        private ISourceRepository sourceRepository;
        private IExerciseRepository exerciseRepository;
        public PublishOfficialManager(IPublishOfficialRepository publishOfficialRepository, ICognitivePartRepository cognitivePartRepository, ISourceRepository sourceRepository, IExerciseRepository exerciseRepository)
        {
            this.publishOfficialRepository = publishOfficialRepository;
            this.cognitivePartRepository = cognitivePartRepository;
            this.sourceRepository = sourceRepository;
            this.exerciseRepository = exerciseRepository;
        }

        public void AddPublishOfficial(Classes.PublishOfficial publishOfficial)
        {
            if (string.IsNullOrWhiteSpace(publishOfficial.Text))
                throw new Exception("You haven't entered text");
            publishOfficialRepository.AddPublishOfficial(publishOfficial);
        }

        public void AcceptPublishOfficial(Classes.PublishOfficial publishOfficial)
        {
            //string publication_type = 
            switch (publishOfficial.Publication.GetType().Name)
            {
                case nameof(CognitivePart):
                    cognitivePartRepository.MakeCognitivePartOfficial((CognitivePart)publishOfficial.Publication);
                    break;
                case nameof(Source):
                    sourceRepository.MakeSourceOfficial((Source)publishOfficial.Publication);
                    break;
                case nameof(Exercise):
                    exerciseRepository.MakeExerciseOfficial((Exercise)publishOfficial.Publication);
                    break;
            }

            publishOfficialRepository.DeletePublishOfficialByPublication(publishOfficial.Publication);
        }

        public void DeclinePublishOfficial(Classes.PublishOfficial makePublicationOfficial)
        {
            publishOfficialRepository.DeletePublishOfficial(makePublicationOfficial);
        }

        public void DeletePublishOfficialsByPublication(TitledPublication titledPublication)
        {
            publishOfficialRepository.DeletePublishOfficialByPublication(titledPublication);
        }

        public Classes.PublishOfficial[] GetPublishOfficialByUser(User user)
        {
            return publishOfficialRepository.GetPublishOfficialByUser(user);
        }
    }
}
