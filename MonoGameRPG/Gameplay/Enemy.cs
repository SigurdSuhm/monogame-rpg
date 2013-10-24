#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using MonoGameRPG.Graphics;
using MonoGameRPG.Physics;

#endregion

namespace MonoGameRPG.Gameplay
{
    public class Enemy : Entity
    {
        #region Constants

        // Path to the enemy texture
        private const string TEXTURE_PATH = "Textures/Enemies/Enemy1";
        // Path to the animation Xml file
        private const string ANIMATION_FILE_NAME = "Enemy1.xml";

        #endregion

        #region Fields

        private int damage;
        private float attackTimer;
        double timeSinceAttack;

        #endregion

        #region Properties

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public float AttackTimer
        {
            get { return attackTimer; }
            set { attackTimer = value; }
        }

        public double TimeSinceAttack
        {
            get { return timeSinceAttack; }
            set { timeSinceAttack = value; }
        }

        #endregion

        #region Constructors

        public Enemy(string name)
            : base(name)
        {
            image = new Image(TEXTURE_PATH);

            damage = 0;
            attackTimer = 1000.0f;
            timeSinceAttack = 0.0f;
        }

        #endregion

        #region Methods

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            // Load animation data
            image.AnimationManager.LoadAnimationData(ANIMATION_FILE_NAME);
            // Create bounding box
            BoundingShape = new BoundingBoxAA(Position, new Vector2(image.AnimationManager.SpriteElementDimensions.X,
                image.AnimationManager.SpriteElementDimensions.Y) + Position);
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceAttack += gameTime.ElapsedGameTime.TotalMilliseconds;

            base.Update(gameTime);
        }

        #endregion
    }
}
